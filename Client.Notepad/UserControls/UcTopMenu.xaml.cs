using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FineEx.Cloud.ClientApp.UserControls
{
    /// <summary>
    /// UcTopMenu.xaml 的交互逻辑
    /// </summary>
    public partial class UcTopMenu : UserControl
    {
        public UcTopMenu()
        {
            InitializeComponent();
        }

        public Window OperatWindow { get; set; }

        public WindowState WindowState { get; set; }

        public bool IsClose { get; set; }

        #region 事件


        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

            if (OperatWindow != null)
                OperatWindow.Close();
        }

        /// <summary>
        /// 最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_max_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;

            ShowOrhide();
        }

        /// <summary>
        /// 正常大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_normal_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;

            ShowOrhide();
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_min_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            ShowOrhide();
        }

        /// <summary>
        /// 双击最大化或者是还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
                ShowOrhide();
            }
        }

        /// <summary>
        /// 显示或者隐藏按钮
        /// </summary>
        private void ShowOrhide()
        {
            switch (WindowState)
            {
                case WindowState.Normal:
                    btn_normal.Visibility = Visibility.Collapsed;
                    btn_max.Visibility = Visibility.Visible;
                    break;
                case WindowState.Minimized:
                    break;
                case WindowState.Maximized:
                    btn_max.Visibility = Visibility.Collapsed;
                    btn_normal.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }

            if (OperatWindow != null)
                OperatWindow.WindowState = WindowState;
        }

        #endregion
    }
}
