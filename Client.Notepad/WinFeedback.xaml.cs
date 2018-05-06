using Client.Notepad.Tools;
using Client.Pub.ServiceProxyFactoryLib;
using System;
using System.Windows;
using YpeuEe.PublicContract.Message;
using YpeuEe.PublicModel.Message;

namespace Client.Notepad
{
    /// <summary>
    /// WinFeedback.xaml 的交互逻辑
    /// </summary>
    public partial class WinFeedback : Window
    {
        public WinFeedback()
        {
            InitializeComponent();

            //移动
            MouseLeftButtonDown += (sender, e) =>
            {
                try
                {
                    DragMove();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            };
            BtnClose.Click += (sender, e) =>
            {
                if (MessageBox.Show("你的反馈就是对我们最大的支持,确定放弃反馈吗？", "信息", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    Close();
            };
            TxtContent.TextChanged += (sender, e) =>
                {
                    LblLenght.Content = $"还可以录入{500 - TxtContent.Text.Length}个字";
                };
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            MFeedback model = new MFeedback();
            model.UserId = SystemCommon.UserInfo == null ? 0 : SystemCommon.UserInfo.Id;
            model.Title = TxtTitle.Text.Trim();
            model.Content = TxtContent.Text;

            IFeedback iFeedback = SPF.Create<IFeedback>("YpeuEe.ServerService.Message.SFeedback", RCT.NoneMode);

            try
            {
                bool result = iFeedback.Submit(model);

                if (result)
                {
                    MessageBox.Show("提交成功！你的反馈就是对我们最大的支持。", "信息", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                    return;
                }

                MessageBox.Show("提交失败。", "信息", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
