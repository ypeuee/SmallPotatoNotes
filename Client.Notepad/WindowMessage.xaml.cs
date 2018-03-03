using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Client.Notepad
{
    /// <summary>
    /// WindowMessage.xaml 的交互逻辑
    /// </summary>
    public partial class WindowMessage : Window, INotifyPropertyChanged
    {
        public WindowMessage()
        {
            InitializeComponent();

            //移动
            MouseLeftButtonDown += (sender, e) =>
            {
                DragMove();
            };
        }



        public WindowMessage(string button1, string button2, string button3) : this()
        {


            Button1 = button1;
            Button2 = button2;
            Button3 = button3;


        }

        private string _button1;
        /// <summary>
        /// 第一个按钮
        /// </summary>
        public string Button1
        {
            get { return _button1; }
            set { _button1 = value; OnPropertyChanged("Button1"); }
        }


        private string _button2;
        /// <summary>
        /// 第二个按钮
        /// </summary>
        public string Button2
        {
            get { return _button2; }
            set { _button2 = value; OnPropertyChanged("Button2"); }
        }




        private string _button3;
        /// <summary>
        /// 第三个按钮
        /// </summary>
        public string Button3
        {
            get { return _button3; }
            set
            {
                _button3 = value;
                OnPropertyChanged("Button3");
            }
        }



        /// <summary>
        /// 操作的按钮
        /// </summary>
        public string OperatingButton { get; set; }

        ///// <summary>
        ///// 标题
        ///// </summary>
        //public string  Title { get; set; }

        private string _title1;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title1
        {
            get { return _title1; }
            set { _title1 = value; OnPropertyChanged("Title"); }
        }


        /// <summary>
        /// 内容
        /// </summary>

        private string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged("Text"); }
        }




        /// <summary>
        /// 事件--单击所有的按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            OperatingButton = ((Button)sender).Content.ToString();
            Close();
        }

        #region 改绑定的值自动更新值

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }


}
