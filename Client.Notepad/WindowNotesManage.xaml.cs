using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Client.Notepad.Tools;
using Client.Notepad.UserControls;
using FineEx.Cloud.ClientApp.UserControls;

namespace Client.Notepad
{
    /// <summary>
    /// WindowNotesManage.xaml 的交互逻辑
    /// </summary>
    public partial class WindowNotesManage : Window
    {
        public WindowNotesManage()
        {
            InitializeComponent();
            //移动
            MouseLeftButtonDown += (sender, e) => { DragMove(); };

        }

        private UcNotepadGroups _ucNotepadGroups;
        private void UcMainMenu1_OnMouseDownEvent(object sender, MouseButtonEventArgs e, EnumMainMenu enummainmenu)
        {
            UserControl uc;
            foreach (var c in GridWork.Children)
            {
                uc = (c as UserControl);
                if (uc != null)
                {
                    uc.Visibility = Visibility.Collapsed;
                }
            }
            switch (enummainmenu)
            {
                //    //关于
                //    case EnumMainMenu.About:
                //        if (_ucAbout == null)
                //        {
                //            _ucAbout = new UcAbout();
                //            _ucAbout?.SetSocketType(SignalRType);
                //            _ucAbout.Computer = ComputerManage.Computer;
                //        }
                //        ShowView(_ucAbout);
                //        break;
                //    //称重
                //    case EnumMainMenu.WeightMeter:
                //        if (_ucWeightMeter == null)
                //            _ucWeightMeter = new UcWeightMeter();
                //        ShowView(_ucWeightMeter);
                //        break;
                //视频
                case EnumMainMenu.Video:
                    if (_ucNotepadGroups == null)
                        _ucNotepadGroups = new UcNotepadGroups();
                    ShowView(_ucNotepadGroups);
                    break;
            }
        }
        /// <summary>
        /// 显示页面
        /// </summary>
        /// <param name="uc">页面</param>
        private void ShowView(UserControl uc)
        {
            if (GridWork.Children.Contains(uc))
            {
                uc.Visibility = Visibility.Visible;
            }
            else
            {
                // uc = new UcVideo();
                GridWork.Children.Add(uc);
            }
        }

    }
}
