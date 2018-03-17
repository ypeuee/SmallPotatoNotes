using Client.Notepad.Tools;
using System;
using System.ComponentModel;
using System.Windows;

namespace Client.Notepad
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WindowMain : Window
    {
        public WindowMain()
        {
            InitializeComponent();

            _settings = new WindowApplicationSettings();

        }

        private WindowApplicationSettings _settings;

        #region 事件

        /// <summary>
        /// 窗体加载，打开所有便签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //加载设置
            _settings.Reload();
            Title = _settings.Title;
            Left = _settings.WinLocation.X;
            Top = _settings.WinLocation.Y;

            RichTextBoxTool.AddExtenstion();
            NotepadManage.OpenAllNotepad();

            Top = -500;
            Left = -500;


            //设置主窗体
            SystemCommon.SetWindowMain(this);
            //设置主窗体
            ShowInTaskbar = !NotepadManage.SystemSetting.ShowInTaskbar;
            //打开导入的文件
            NotepadManage.OpenImputFileTask();

            new WindowNotesManage().Show();
        }

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            //保存设置
            _settings.Title = Title;
            _settings.WinLocation = new Point(Left, Top);
            _settings.Save();

            //保存所有便签设置--已经在APP中设置
            NotepadManage.SaveSetings();

            //关闭一个应用程序。
            SystemCommon.ExitSystem();


        }

        /// <summary>
        /// 激活所有便签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Activated(object sender, EventArgs e)
        {
            //this.WindowState = WindowState.Minimized;
            NotepadManage.ActivatedNotepad();

            Top = -500;
            Left = -500;
        }
        #endregion





    }




}
