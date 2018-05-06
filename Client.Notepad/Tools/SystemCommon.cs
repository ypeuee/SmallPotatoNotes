using System;
using YpeuEe.PublicModel.Message;

namespace Client.Notepad.Tools
{
    /// <summary>
    /// 系统公共类
    /// </summary>
    class SystemCommon
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        internal static MUserInfo UserInfo { get; set; }

        private static string _systemName;
        /// <summary>
        /// 系统的名称
        /// </summary>
        public static string SystemName
        {
            get
            {
                if (string.IsNullOrEmpty(_systemName))
                    _systemName = "小土豆便签";

                return _systemName;
            }
        }

        /// <summary>
        /// 设置主窗体
        /// </summary>
        /// <param name="windowMain"></param>
        public static void SetWindowMain(WindowMain windowMain)
        {
            WindowMain = windowMain;
        }

        /// <summary>
        /// 主窗体
        /// </summary>
        public static WindowMain WindowMain { get; private set; }


        private static string _extension;
        /// <summary>
        /// 缓存文件扩展名
        /// </summary>
        public static string Extension
        {
            get
            {
                if (string.IsNullOrEmpty(_extension))
                    _extension = ".xtd";

                return _extension;
            }
        }

        private static string _searchExtensionName;
        /// <summary>
        /// 查询缓存文件的名称
        /// *+扩展名
        /// </summary>
        public static string SearchExtensionName
        {
            get
            {
                if (string.IsNullOrEmpty(_searchExtensionName))
                    _searchExtensionName = "*" + Extension;

                return _searchExtensionName;
            }
        }


        private static bool _isExitSystem;
        /// <summary>
        /// 是否 true 通知系统关闭
        /// </summary>
        public static bool IsExitSystem
        {
            get { return _isExitSystem; }
            set
            {
                _isExitSystem = value;
            }
        }

        /// <summary>
        /// 通知系统关闭并退出系统
        /// </summary>
        public static void ExitSystem()
        {
            if(IsExitSystem)
                return;

            IsExitSystem = true;
            // Environment.Exit(0);
            //System.Windows.Application.Current.Shutdown(0);
            System.Windows.Application.Current.Shutdown();
        }


        //下载版本不同的文件
        private static string currentPath;
        private static string iisBinPath;
        /// <summary>
        /// 系统路径
        /// </summary>
        public static string Path
        {
            get
            {
                if (string.IsNullOrEmpty(iisBinPath) && string.IsNullOrEmpty(currentPath))
                {
                    currentPath = AppDomain.CurrentDomain.BaseDirectory;
                    iisBinPath = AppDomain.CurrentDomain.RelativeSearchPath;
                }

                return string.IsNullOrEmpty(iisBinPath) ? currentPath : iisBinPath;
            }
        }

        private static string _applicationName;
        /// <summary>
        /// 程序的完整路径包括名称及扩展名
        /// </summary>
        public static string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationName))
                    _applicationName = System.Reflection.Assembly.GetExecutingAssembly().Location;

                //System.Reflection.Assembly.GetExecutingAssembly().Location;
                //System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

                return _applicationName;
            }

        }

        private static string _fileName;
        /// <summary>
        /// 程序名称
        /// 没有扩展名，没有路径，只有一个名称
        /// </summary>
        public static string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(_fileName))
                    _fileName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

                return _fileName;
            }

        }




    }
}
