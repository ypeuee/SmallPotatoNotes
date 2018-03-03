using System;
using System.Windows;
using System.ComponentModel;
using Client.Notepad.Tools;
using Commons.Tools.Tools;

namespace Client.Notepad
{
    /// <summary>
    /// WindowSet.xaml 的交互逻辑
    /// </summary>
    public partial class WindowSet : Window, INotifyPropertyChanged
    {


        /// <summary>
        /// 设置页面
        /// </summary>
        /// <param name="windowNotepad">操作页面</param>
        public WindowSet(WindowNotepad windowNotepad) : this()
        {
            WindowNotepad = windowNotepad;
            Opacity = windowNotepad.Opacity;
            GridTitleBottom.Height = windowNotepad.TitleBottomHeight;
            TextBlock1.FontSize = windowNotepad.TxtTitleBottom.FontSize;
            Boot = AutoBoot.GetAutoBootStatu(SystemCommon.SystemName);

        }

        /// <summary>
        /// 设置页面
        /// </summary>
        public WindowSet()
        {
            InitializeComponent();

            //移动
            MouseLeftButtonDown += (sender, e) =>
            {
                DragMove();
            };

            //关闭
            BtnClose.Click += (sender, e) => { Close(); };

            //确认
            BtnCheck.Click += (sender, e) =>
            {
                foreach (WindowNotepad notepad in NotepadManage.WindowList)
                {
                    notepad.ShowInTaskbar = SystemSetting.ShowInTaskbar;

                    if (ChkNotepadAll.IsChecked == true || notepad.Equals(WindowNotepad))
                    {
                        notepad.Opacity = SliderOpacity.Value;
                        notepad.TitleBottomHeight = SliderTitleBottom.Value;
                        notepad.TxtTitleBottom.FontSize = SliderTitleBottomFontSize.Value;

                        if (notepad.Top < 0)
                        {
                            notepad.GridTitleBottom.Height = SliderTitleBottom.Value;


                            while ((Int64)notepad.Top != (Int64)(-notepad.ActualHeight + notepad.TitleBottomHeight))
                            {
                                notepad.Top += notepad.Top >= -notepad.ActualHeight + notepad.TitleBottomHeight ? -1 : 1;
                            }
                        }
                    }
                }

                //设置主窗体
                SystemCommon.WindowMain.ShowInTaskbar = !SystemSetting.ShowInTaskbar;

                //非调试模式
                if (!System.Diagnostics.Debugger.IsAttached)
                {
                    try
                    {
                        if (Boot == true)
                        {
                            //开机启动
                            ShortcutCreator.CreateShortcutOnDesktop(SystemCommon.SystemName);

                            if (!AutoBoot.SetAutoBootStatu1(true, SystemCommon.SystemName))
                                AutoBoot.SetAutoBootStatu(true, SystemCommon.SystemName);
                        }
                        else
                        {
                            //取消开机启动
                            AutoBoot.SetAutoBootStatu1(false, SystemCommon.SystemName);
                            AutoBoot.SetAutoBootStatu(false, SystemCommon.SystemName);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                Close();
            };
        }

        public WindowNotepad WindowNotepad { get; set; }


        private bool? _boot;
        /// <summary>
        /// 是否开机启动
        /// </summary>
        public bool? Boot
        {
            get { return _boot; }
            set { _boot = value; OnPropertyChanged("Boot"); }
        }



        /// <summary>
        /// 系统设置
        /// </summary>
        public SystemSettingM SystemSetting
        {
            get { return NotepadManage.SystemSetting; }
            set
            {
                NotepadManage.SystemSetting = value;
                OnPropertyChanged("SystemSetting");
            }
        }


        #region 改绑定的值自动更新值

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 在更改属性值时发生
        /// </summary>
        /// <param name="propertyName"></param>
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
