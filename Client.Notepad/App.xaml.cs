using Client.Notepad.Tools;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Notepad
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {


            #region 程序退出异常处理

            var pro = Process.GetCurrentProcess();
            pro.EnableRaisingEvents = true;

            pro.Exited += (sender, e1) =>
            {
                ToolLogs.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "在进程退出时发生");
            };

            AppDomain.CurrentDomain.ProcessExit += (sender, e1) =>
            {
                ////晚于this.Exit
                //System.Threading.Monitor.TryEnter(obj, 500);
                ////后来改为 
                //System.Threading.Monitor.Enter(obj);

                ToolLogs.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "当默认应用程序域的父进程存在时发生");

                ////不能在这保存所有便签设置，因为这时所有的程序已经全部关闭。
                //NotepadManage.SaveSetings();
            };

            //异常处理
            this.DispatcherUnhandledException += (sender, e1) =>
           {

               ToolLogs.Debug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "系统异常处理", e1.Exception);

               e1.Handled = true;//使用这一行代码告诉运行时，该异常被处理了，不再作为UnhandledException抛出了。
               SystemCommon.IsExitSystem = true;

               MessageBox.Show("我们很抱歉，当前应用程序遇到一些问题，该操作已经终止,将要重新启动。" + e1.Exception.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);//这里通常需要给用户一些较为友好的提示，并且后续可能的操作


               //保存所有便签设置
               NotepadManage.SaveSetings();

               //不要在这打开程序，如果你的程序是刚打开时出现错误，用户将无法关闭。这将是用户的恶梦。
               SystemCommon.ExitSystem();
               //System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
           };

            this.Exit += (sender, e1) =>
            {
                //早于AppDomain.CurrentDomain.ProcessExit

                ////不能在这保存所有便签设置，因为这时所有的程序已经全部关闭。
                // NotepadManage.SaveSetings();
                //e1.ApplicationExitCode
            };

            #endregion

            bool result = ImputCacheFile();

            //非调试模式
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                Client.Pub.ServiceProxyFactoryLib.ProxyBinding.ReplaceAddress = true;
                Client.Pub.ServiceProxyFactoryLib.ProxyBinding.ReplacePort = true;

                //检测是否已有一个程序实例运行
                this.Startup += new StartupEventHandler(App_Startup);


                //下载版本不同的文件
                string loadPath = Path.Combine(SystemCommon.Path, "AutoUpdate");

                //复制自动更新程序文件
                Tools.FileManage.CopyDir(Path.Combine(loadPath, "LoadDown\\AutoUpdate\\"), loadPath);

                Tools.FileManage.IsUpdateThread();

                //绑定扩展名
                Task task = new Task(() =>
                 {
                     try
                     {
                         BindingExtend();
                     }
                     catch (Exception e)
                     {
                         ToolLogs.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "绑定扩展名", e);
                     }

                 });
                task.Start();
            }

        }

        /// <summary>
        /// 导入缓存文件
        /// </summary>
        private bool ImputCacheFile()
        {
            // 摘要: 返回包含当前进程的命令行参数的字符串数组。
            // 返回结果:  字符串数组，其中的每个元素都包含一个命令行参数。第一个元素是可执行文件名，后面的零个或多个元素包含其余的命令行参数。
            string[] strs = Environment.GetCommandLineArgs();

            int result = 0;
            if (strs.Length <= 1)
            {
                return false;
            }

            for (int i = 1; i < strs.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(strs[i]);
                string newPaht = RichTextBoxTool.PathCache + fileInfo.Name;
                if (File.Exists(newPaht))
                {
                    ToolLogs.Debug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "导入失败，文件已经存在" + strs[i]);
                    result++;
                }
                else
                {
                    fileInfo.MoveTo(newPaht);
                    ToolLogs.Debug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "导入成功！" + strs[i]);
                    //fileInfo.Delete();
                }
            }
            //if (result > 0)
            //{
            //    MessageBox.Show("有" + result + "个缓存文件导入失败，因为这此文件已经在系统中存在", "导入结果", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}

            return true;

        }

        /// <summary>
        /// 绑定扩展名
        /// </summary>
        private void BindingExtend()
        {
            if (!FileTypeRegister.FileTypeRegistered(SystemCommon.Extension))
            {
                FileTypeRegInfo fileTypeRegInfo = new FileTypeRegInfo(SystemCommon.Extension);
                fileTypeRegInfo.Description = SystemCommon.SystemName + "缓存文件";
                fileTypeRegInfo.ExePath = SystemCommon.ApplicationName;
                fileTypeRegInfo.ExtendName = SystemCommon.Extension;
                fileTypeRegInfo.IconPath = SystemCommon.ApplicationName;

                // 注册  
                //FileTypeRegister fileTypeRegister = new FileTypeRegister();
                FileTypeRegister.RegisterFileType(fileTypeRegInfo);
            }
        }

        //开始等待图片
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    SplashScreen s = new SplashScreen("20170904144908.gif");
        //    s.Show(true);

        //    base.OnStartup(e);
        //}

        System.Threading.Mutex _mutex;
        /// <summary>
        /// 检测是否已有一个程序实例运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                bool ret;
                _mutex = new System.Threading.Mutex(true, SystemCommon.FileName, out ret);
                // _mutex.ReleaseMutex();
                if (!ret)
                {

                    //MessageBox.Show("已有一个程序实例运行");
                    Environment.Exit(0);
                    // SystemCommon.ExitSystem();
                    //Application.Current.Shutdown();
                }

            }
            catch (Exception exception)
            {
                ToolLogs.Debug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "检测是否已有一个程序实例运行", exception);
            }


        }


    }
}
