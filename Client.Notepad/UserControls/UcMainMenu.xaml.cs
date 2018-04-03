using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Commons.Tools.Tools;

namespace FineEx.Cloud.ClientApp.UserControls
{
    /// <summary>
    /// UcMainMenu.xaml 的交互逻辑
    /// </summary>
    public partial class UcMainMenu : UserControl
    {
        public UcMainMenu()
        {
            InitializeComponent();
        }
        public delegate void ImageMouseDown(object sender, MouseButtonEventArgs e, EnumMainMenu enumMainMenu);

        /// <summary>
        /// 鼠标单击事件
        /// </summary>
        [Description("单击菜单按键"), Category("自定义")]
        public new event ImageMouseDown MouseDownEvent;


        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetImage(SelectImage);
            SetImage((Image)sender, "2");

            SelectImage = (Image)sender;
            if (MouseDownEvent != null)
            {
                EnumMainMenu enumMainMenu;
                switch (((Image)sender).ToolTip.ToString())
                {
                    case "头像":
                        enumMainMenu = EnumMainMenu.Avatar;
                        break;
                    case "管理":
                        enumMainMenu = EnumMainMenu.Manage;
                        break;
                    case "视频":
                        enumMainMenu = EnumMainMenu.Video;
                        break;
                    case "关于":
                        enumMainMenu = EnumMainMenu.About;
                        break;
                    default:
                        enumMainMenu = EnumMainMenu.About;
                        break;

                }
                MouseDownEvent(sender, e, enumMainMenu);
            }
        }

        private Image SelectImage;
        private void SetImage(Image image, string index = "1")
        {
            if (image == null) return;
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri($"../Images/{image.ToolTip}{index}.png", UriKind.Relative);
            bi.EndInit();

            image.Source = bi;
        }


    }
}
