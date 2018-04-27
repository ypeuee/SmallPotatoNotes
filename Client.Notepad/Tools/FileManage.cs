using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using Client.Pub.ServiceProxyFactoryLib;
using Common.Contract.AutoUpdate;
using Common.DataType.AutoUpdate;

namespace Client.Notepad.Tools
{
    public class FileManage : BaseClass
    {

        #region 复制文件

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="fromDir"></param>
        /// <param name="toDir"></param>
        public static void CopyDir(string fromDir, string toDir)
        {
            if (!Directory.Exists(fromDir))
                return;

            if (!Directory.Exists(toDir))
            {
                Directory.CreateDirectory(toDir);
            }

            string[] files = Directory.GetFiles(fromDir);
            foreach (string formFileName in files)
            {
                string fileName = Path.GetFileName(formFileName);
                string toFileName = Path.Combine(toDir, fileName);
                try
                {
                    File.Copy(formFileName, toFileName, true);
                    File.Delete(formFileName);
                }
                catch (Exception ex)
                {
                    ToolLogs.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "复制文件1", ex);
                }
            }
            string[] fromDirs = Directory.GetDirectories(fromDir);
            foreach (string fromDirName in fromDirs)
            {
                string dirName = Path.GetFileName(fromDirName);
                string toDirName = Path.Combine(toDir, dirName);
                CopyDir(fromDirName, toDirName);

                try
                {
                    Directory.Delete(fromDirName);
                }
                catch (Exception ex)
                {
                    ToolLogs.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "复制文件2", ex);
                }

            }
        }

        #endregion

        #region 窗体关闭事件，启动主程序

        /// <summary>
        ///   启动主程序并终止所有进程
        /// </summary>
        public static void StartMainApplication()
        {


            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string iisBinPath = AppDomain.CurrentDomain.RelativeSearchPath;
            string loadPath = string.IsNullOrEmpty(iisBinPath) ? currentPath : iisBinPath;

            loadPath = Path.Combine(loadPath, "AutoUpdate");

            //ConfigurationManager.OpenExeConfiguration
            string runApplication = "Client.AutoUpdate.exe";

            try
            {
                //启动程序
                Process pr = new Process();
                pr.StartInfo.FileName = Path.Combine(loadPath, runApplication);
                pr.Start(); //运行

                Environment.Exit(0); // 终止此进程并为基础操作系统提供指定的退出代码。
            }
            catch (Exception ex)
            {
                ToolLogs.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "启动主程序并终止所有进程", ex);

                MessageBox.Show(@"启动更新程序失败，请检查配置文件是否正确。", @"错误");
                return;
            }


        }

        #endregion

        #region 检查是否有更新

        /// <summary>
        /// 线程检查是否有更新
        /// 一般用于系统自动调用
        /// </summary>
        public static void IsUpdateThread()
        {
            //检测并自动更新
            Thread t = new Thread(IsUpdateT);
            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        /// 后台检查是否有更新
        /// 一般用于系统自动调用
        /// </summary>
        public static void IsUpdateT()
        {
            do
            {
                string load = GetVersionLoad();
                string service = GetVersionService(SystemName);

                //检测本地版本与服务器版本是否一样
                if (service != null && load != service)
                {

                    if (MessageBox.Show("童鞋，有更新了，要不要试试？ 本地版本：" + load + "服务器版本：" + service, "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        StartMainApplication();
                    }
                    else
                    {
                        return;
                    }
                }

                Thread.Sleep(60 * 1000 * 10);
            } while (true);
        }


        static string SystemName = "桌面便签";
        static string filePathVersion = "AutoUpdate\\SystemVersion.xml";
        /// <summary>
        /// 前台检查是否有更新
        /// 一般用于用户主动发起
        /// </summary>
        /// <returns></returns>
        public static bool IsUpdate()
        {
            string load = GetVersionLoad();
            string service = GetVersionService(SystemName);

            //MessageBox.Show("load" + load + "service" + service);
            if (service == null)
            {
                MessageBox.Show("悲剧了，无法连接到服务器！");
                //启动主程序
                return false;
            }

            //检测本地版本与服务器版本是否一样
            if (load == service)
            {
                MessageBox.Show("童鞋，你这已经是最新版本了，不错不错，看来你已经跟上潮流了，加油！你是以后潮流的前沿者！ 本地版本：" + load + "服务器版本：" + service);
                //启动主程序
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取版本信息--服务器
        /// </summary>
        /// <returns></returns>
        private static string GetVersionService(string systemName)
        {
            try
            {
                IVersionManage iVersionManage = SPF.Create<IVersionManage>("Server.ServiceLayer.AutoUpdate.VersionManageSL", RCT.NoneMode);
                string version = iVersionManage.GetVersion(systemName);

                return version;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        /// <summary>
        /// 获取版本信息--本地
        /// </summary>
        /// <returns></returns>
        private static string GetVersionLoad()
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string iisBinPath = AppDomain.CurrentDomain.RelativeSearchPath;
            string loadPath = string.IsNullOrEmpty(iisBinPath) ? currentPath : iisBinPath;

            try
            {
                VersionM versionM = XMLSerializer.Deserialize<VersionM>(Path.Combine(loadPath, filePathVersion));

                return versionM?.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        #endregion
    }
}
