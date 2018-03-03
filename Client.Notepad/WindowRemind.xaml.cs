using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Client.Notepad.UserControls;

namespace Client.Notepad
{
    /// <summary>
    /// WindowRemind.xaml 的交互逻辑
    /// </summary>
    public partial class WindowRemind : Window//, INotifyPropertyChanged
    {
        public WindowRemind(string caption, DateTime? remindDateTime, CancellationTokenSource cts)
        {
            InitializeComponent();

            //移动
            MouseLeftButtonDown += (sender, e) =>
            {
                DragMove();
            };

            _cts = cts;
            RemindDateTime = remindDateTime;
             TxtCaption.Text ="    "+ caption;
         
            Caption = caption;

            if (RemindDateTime == null)
                RemindDateTime = DateTime.Now.AddMinutes(10);

            Calendar1.SelectedDate = RemindDateTime;
            UcTimeSpanPicker1.SetValue(UcTimeSpanPicker.TimeSpanProperty, new TimeSpan(((DateTime)RemindDateTime).Hour, ((DateTime)RemindDateTime).Minute, 0));
           

            //时间显示
            DispatcherTimer time = new DispatcherTimer();
            time.Interval = TimeSpan.FromSeconds(1);
            time.Tick += (sender, e) =>
            { 
                TextBlock1.Text = DateTime.Now.TimeOfDay.ToString();
            };
            time.Start();
        }

        private string _caption;
        /// <summary>
        /// 标题
        /// </summary>
        public string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                //OnPropertyChanged("Caption");
            }
        }

        public Action RemindAction;

        /// <summary>
        /// 提醒的时间
        /// </summary>
        public DateTime? RemindDateTime { get; set; }

        /// <summary>
        /// true保存设置 false不保存
        /// </summary>
        public bool IsSave { get; set; }

        /// <summary>
        /// 用于取消已经运行的任务
        /// </summary>
        private CancellationTokenSource _cts;

        /// <summary>
        /// 创建提醒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public string CreateRemind(DateTime dateTime)
        {
            if (dateTime <= DateTime.Now)
            {
                return "提醒时间必需大于当前时间！";
            }

            RemindDateTime = dateTime;
            //如果已经启动已经任务，则先取消
            if (_cts != null)
                _cts.Cancel();
            _cts = new CancellationTokenSource();

            ////_task = new Task(aaa, DateTime, ct);
             
            var ct = _cts.Token;
            Task task = new Task(() =>
            {
                while (dateTime >= DateTime.Now)
                {
                    Thread.Sleep(1000);

                    //因为这个时候收到Cancel的消息
                    ct.ThrowIfCancellationRequested();
                }

                if (RemindAction != null)
                    RemindAction();
                 
            }, ct);

            try
            {
                task.Start();
                //_task.Wait();
            }
            catch (AggregateException ex)
            {

                //foreach (var e in ex.InnerExceptions)
                //{
                //    Console.WriteLine("我是OperationCanceledException：{0}", e.Message);
                //}
            }

            return string.Empty;
        }

        //事件-立即生效
        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (Calendar1.SelectedDate == null)
                Calendar1.SelectedDate = DateTime.Now;

            string str = ((DateTime)Calendar1.SelectedDate).ToString("yyyy-MM-dd ") + UcTimeSpanPicker1.TimeSpan;

            DateTime dateTime = DateTime.Parse(str);

            string msg = CreateRemind(dateTime);
            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg, "警告", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            //DialogResult = true;
            IsSave = true;
            Close();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;
            Close();
        }

        //#region 改绑定的值自动更新值

        //public event PropertyChangedEventHandler PropertyChanged;
        //public virtual void OnPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
        //#endregion
    }
}
