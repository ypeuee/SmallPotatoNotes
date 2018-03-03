using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Client.Notepad.Tools;
using Application = System.Windows.Application;
using Clipboard = System.Windows.Clipboard;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using RichTextBox = System.Windows.Controls.RichTextBox;

namespace Client.Notepad
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class WindowNotepad : Window, INotifyPropertyChanged
    {
        #region 构造
        /// <summary>
        /// 便签页
        /// </summary>
        public WindowNotepad()
            : this(string.Empty, 0)
        {

        }

        /// <summary>
        /// 便签页
        /// </summary>
        /// <param name="id">D</param>
        /// <param name="index">索引</param>
        public WindowNotepad(string id, int index)
        {
            InitializeComponent();
            //Canvas.SetZIndex(GridTitleBottom, 1);

            ID = id;
            Index = index;
            TitleBottomHeight = GridTitleBottom.Height;

            if (string.IsNullOrEmpty(ID))
                ID = RichTextBoxTool.PathNewCacheFileName;

            CacheFileName = Path.Combine(RichTextBoxTool.PathCache, ID) + SystemCommon.Extension;

            SystemSetting = NotepadManage.SystemSetting;
            SkinM = new SkinManage().GetRandom();

        }

        #endregion

        #region 属性

        /// <summary>
        /// 用于取消已经运行的任务
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;


        private SystemSettingM _systemSetting;

        /// <summary>
        /// 系统设置
        /// </summary>
        public SystemSettingM SystemSetting
        {
            get { return _systemSetting; }
            set
            {
                _systemSetting = value;
                OnPropertyChanged("SystemSetting");
            }
        }

        /// <summary>
        /// 隐藏后标题的高
        /// </summary>
        public double TitleBottomHeight { get; set; }

        /// <summary>
        /// ID 
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 显示的索引
        /// </summary>
        public int Index { get; set; }

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
                OnPropertyChanged("Caption");
            }
        }

        public bool WindowTopmost
        {
            get { return Topmost; }
            set
            {
                Topmost = value;

                // 置顶图片处理
                if (Topmost)
                    BtnTopmost.Source = new BitmapImage(new Uri("/Images/lock_32px.png", UriKind.Relative));
                else
                    BtnTopmost.Source = new BitmapImage(new Uri("/Images/unlock_32px.png", UriKind.Relative));
            }
        }

        /// <summary>
        /// 缓存本地文件的名称
        /// </summary>
        public string CacheFileName { get; set; }

        public WindowsTool WindowsTool { get; private set; }

        private SkinM _skinM;

        public SkinM SkinM
        {
            get { return _skinM; }
            set
            {
                _skinM = value;
                OnPropertyChanged("SkinM");
            }
        }

        /// <summary>
        /// 提醒的时间
        /// </summary>
        public DateTime? RemindDateTime { get; set; }

        #endregion

        #region 事件

        #region 窗体操作事件

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window1_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(CacheFileName))
            {
                RichTextBoxTool.Read(RichTextBox1, CacheFileName);
                SetCaption();
            }
            //设置便签
            NotepadManage.SetWindowNotepad(this);
            //设置提醒
            if (RemindDateTime != null)
                SetRemind((DateTime)RemindDateTime);
            //窗体效果设置
            WindowsTool tool = new WindowsTool(this);
            tool.SetTopAutoShow(GridTitleBottom, GridTitle);
            tool.SetNotMax(GridTitle);
            tool.DragMove(GridTitle);
            WindowsTool = tool;
            RichTextBox1.Focus();

        }

        /// <summary>
        /// 在窗口变成前台窗口时发生。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowNotepad_OnActivated(object sender, EventArgs e)
        {
            NotepadManage.SetIndexOrderBy(Index);

            IsShosButton(true);

            //if (NotepadManage.SystemSetting.ShowInTaskbar && WindowsTool != null)
            //{
            //    WindowsTool.Show(GridTitleBottom);
            //    // base.OnMouseMove( new MouseEventArgs(null, 1));
            //}
        }

        /// <summary>
        /// 在窗口变成后台窗口时发生。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowNotepad_OnDeactivated(object sender, EventArgs e)
        {
            IsShosButton(false);

            //if (NotepadManage.SystemSetting.ShowInTaskbar && WindowsTool != null)
            //{
            //    WindowsTool.Hide(GridTitleBottom);
            //    // base.OnMouseMove( new MouseEventArgs(null, 1));
            //}
        }

        /// <summary>
        /// 关闭前处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window1_OnClosing(object sender, CancelEventArgs e)
        {
            try
            {
                RichTextBoxTool.Writer(RichTextBox1, CacheFileName);

                //如果ShowInTaskbar设置为true，则关闭时先关闭子窗体
                //如果设置为false，则先关闭主窗体
                if (!ShowInTaskbar)
                    NotepadManage.RemoveNotepad(this);

                NotepadManage.ShowWindowListCount--;
                if (NotepadManage.ShowWindowListCount == 0)
                {
                    //关闭一个应用程序。
                    SystemCommon.ExitSystem();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存出了点问题..." + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Cancel = true;
            }
        }

        #endregion

        #region 按键操作事件

        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTopmost_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            WindowTopmost = !WindowTopmost;
        }

        /// <summary>
        /// 创建新页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;
            NotepadManage.CreateNotepad(this);

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

            //如果是空内容，关闭不提示
            string resutStr = RichTextBoxTool.StringFromRichTextBox(RichTextBox1);
            if (string.IsNullOrEmpty(resutStr) || resutStr == Environment.NewLine)
            {
                Close();

                if (ShowInTaskbar)
                    NotepadManage.RemoveNotepad(this);

                if (File.Exists(CacheFileName))
                    File.Delete(CacheFileName);

                return;
            }

            WindowMessage windowMessage = new WindowMessage("删除", "隐藏", "取消");
            //windowMessage.Button1 = "删除";
            //windowMessage.Button2 = "隐藏";
            //windowMessage.Button3 = "取消";
            windowMessage.Text = "确认删除或隐藏？删除之后可在回收站中查找，隐藏并非删除，系统下次启动还会显示。";
            windowMessage.Title = "确认删除或隐藏";
            windowMessage.ShowDialog();
            string ret = windowMessage.OperatingButton;
            switch (ret)
            {
                case "删除":

                    Close();

                    if (ShowInTaskbar)
                        NotepadManage.RemoveNotepad(this);

                    if (File.Exists(CacheFileName))
                        File.Delete(CacheFileName);

                    NotepadManage.WindowListCount--;
                    break;

                case "隐藏":

                    Close();
                    break;
                case "取消":
                    return;
                    break;
            }


        }

        /// <summary>
        /// 已经确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCheck_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SetCheck();
        }

        #endregion

        #region RichTextBox操作事件

        /// <summary>
        /// 鼠标离开时设置标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RichTextBox1_MouseLeave(object sender, MouseEventArgs e)
        {
            SetCaption();
            RichTextBoxTool.Writer(RichTextBox1, CacheFileName);
        }

        /// <summary>
        /// 没有焦点时保存，并设置标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RichTextBox1_OnPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SetCaption();
            RichTextBoxTool.Writer(RichTextBox1, CacheFileName);
        }

        /// <summary>
        /// 事件--输入文件按回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RichTextBox1_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            ClearProperty();

            //第一行为标题，第二行为内容，并添加项目符号
            if (RichTextBox1.Document.Blocks.Count != 2)
                return;

            if (!(RichTextBox1.Document.Blocks.LastBlock is System.Windows.Documents.List))
                ShwoNumber();
        }



        #endregion

        #region 右键菜单操作事件

        /// <summary>
        /// 事件--选择颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemColour_OnClick(object sender, RoutedEventArgs e)
        {
            string header = ((System.Windows.Controls.HeaderedItemsControl)sender).Tag.ToString();
            SkinM = new SkinManage().GetSkin(header);

        }


        /// <summary>
        /// 事件--操作事件包括 添加提醒，添加确认，添加序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemOperating_OnClick(object sender, RoutedEventArgs e)
        {
            string header = ((System.Windows.Controls.HeaderedItemsControl)sender).Tag.ToString();

            switch (header)
            {
                case "添加确认":
                    SetCheck();
                    break;
                case "添加图片":
                    AddImage(RichTextBox1);
                    break;
                case "添加提醒":
                    SetRemind();
                    break;
                case "发送给":
                    var v = new StringCollection();
                    v.Add(CacheFileName);
                    Clipboard.SetFileDropList(v);
                    MessageBox.Show("复制成功，快去聊天工具上粘贴吧Ctrl+C", "发送给...", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case "去格式粘贴":
                    //Class1.Text();
                    //pgn,jpg两种格式FileDrop

                    if (Clipboard.ContainsFileDropList())
                    {

                        e.Handled = true;

                        StringCollection stringCollection = Clipboard.GetFileDropList();
                        foreach (string s in stringCollection)
                        {     //如果是图片，则导入图片
                            RichTextBoxTool.RichTextBoxAddImage(RichTextBox1, s, image_MouseDown);
                        };
                    }
                    else if (Clipboard.ContainsText())
                    {

                        e.Handled = true;
                        // 去格式粘贴
                        //RichTextBoxTool.Paste(RichTextBox1);

                        //IDataObject dataObj = Clipboard.GetDataObject();

                        string paste = Clipboard.GetText();
                        //Clipboard.Clear();
                        Clipboard.SetText(paste);
                        RichTextBox1.Paste();
                        Clipboard.Clear();

                        //Clipboard.SetDataObject(dataObj.GetData(DataFormats.Text));

                        //System.String
                        //    Rich Text Format
                        //Locale
                        //    OEMText
                        //UnicodeText
                    }


                    break;

            }

        }
        private void CommandBinding_Executed()
        {
            if (Clipboard.ContainsImage())
            {
                ;
                Clipboard.Clear();
                return;
            }
            //Get Unicode Text
            string paste = Clipboard.GetText();
            Clipboard.Clear();
            Clipboard.SetText(paste);
            RichTextBox1.Paste();
        }

        /// <summary>
        /// 系统设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSet_OnClick(object sender, RoutedEventArgs e)
        {
            WindowSet window = new WindowSet(this);
            window.ShowDialog();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            if (FileManage.IsUpdate())
            {
                FileManage.StartMainApplication();
            }
        }

        #endregion

        #endregion

        #region 方法

        /// <summary>
        /// 是否显示操作按钮
        /// </summary>
        /// <param name="isShow"></param>
        public void IsShosButton(bool isShow)
        {
            //      if (!IsActive) return;

            Visibility visibility = isShow ? Visibility.Visible : Visibility.Hidden;

            BtnTopmost.Visibility = visibility;
            BtnClose.Visibility = visibility;
            BtnAdd.Visibility = visibility;
            BtnCheck.Visibility = visibility;

        }


        /// <summary>
        /// 设置标题
        /// </summary>
        private void SetCaption()
        {
            string resutStr = RichTextBoxTool.StringFromRichTextBox(RichTextBox1);
            if (string.IsNullOrEmpty(resutStr) || resutStr == Environment.NewLine)
                return;

            try
            {
                string[] strs = resutStr.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                resutStr = strs[0];

                if (resutStr.Length < 10)
                {
                    Caption = resutStr;
                }
                else
                {
                    Caption = resutStr.Substring(0, 10);
                }

                this.Title = Caption;
            }
            catch (Exception)
            {
                return;
            }
        }



        /// <summary>
        /// 图片插入
        /// </summary>
        /// <param name="rtbInfo"></param>
        /// <returns></returns>
        public bool AddImage(RichTextBox rtbInfo)
        {

            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = "*.png|*.jpg";
            ofd.Filter = "png file|*.png|jpg file|*.jpg";
            if (ofd.ShowDialog() == false)
            {
                return false;
            }

            //添加图片
            RichTextBoxTool.RichTextBoxAddImage(RichTextBox1, ofd.FileName, image_MouseDown);

            rtbInfo.Focus();
            return true;

        }

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowShowImage frm = new WindowShowImage(((System.Windows.Controls.Image)sender).Source);
            frm.ShowDialog();
        }

        /// <summary>
        /// 设置为已经确认
        /// </summary>
        private void SetCheck()
        {
            //if ((FontWeight)RichTextBox1.Selection.GetPropertyValue(TextElement.FontWeightProperty) == FontWeights.Bold)
            //    RichTextBox1.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            //else
            //    RichTextBox1.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);


            //var decorations = (TextDecorationCollection)RichTextBox1.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            //bool hasUnderline = decorations.Contains(TextDecorations.Strikethrough[0]);
            //if (hasUnderline)
            //    RichTextBox1.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, FontWeights.Normal);
            //else
            //    RichTextBox1.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Strikethrough);




            var selection = this.RichTextBox1.Selection;
            if (!selection.IsEmpty)
            {
                TextDecorationCollection tdc = selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection;
                if (tdc == null || !(tdc.SequenceEqual(TextDecorations.Strikethrough) || tdc.Contains(TextDecorations.Strikethrough[0])))
                {
                    selection.ApplyPropertyValue(Run.TextDecorationsProperty, TextDecorations.Strikethrough);
                }
                else
                {
                    selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                }
            }
            else
            {
                //RichTextBox1.CaretPosition.InsertTextInRun("aaa");

                //RichTextBox1.Selection.Text = "百度";
                //TextRange tr = new TextRange(RichTextBox1.Selection.Start, RichTextBox1.Selection.End);
                //Hyperlink hlink = new Hyperlink(tr.Start, tr.End);
                //hlink.NavigateUri = new Uri("");


                var textRange = new TextRange(this.RichTextBox1.Document.ContentStart, this.RichTextBox1.Document.ContentEnd);
                if (!textRange.IsEmpty)
                {
                    textRange.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                }

            }

        }

        /// <summary>
        /// 换行后，去除样式
        /// </summary>
        private void ClearProperty()
        {
            //为解决换行后，把上一行的样式带入下一下（思想：1、新插入一下，2、去除插入行的样式，3、移除插入行）
            //System.Windows.Documents.List list = new System.Windows.Documents.List();
            Run run = new Run("");
            Paragraph paragraph = new Paragraph(run);
            RichTextBox1.Document.Blocks.Add(paragraph);
            //记录插入位置
            TextPointer textPointer = RichTextBox1.CaretPosition;

            TextPointer p1 = RichTextBox1.Document.Blocks.LastBlock.ContentStart;
            TextPointer p2 = RichTextBox1.Document.Blocks.LastBlock.ContentEnd;
            this.RichTextBox1.Selection.Select(p1, p2);
            var selection = this.RichTextBox1.Selection;
            selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            RichTextBox1.Document.Blocks.Remove(RichTextBox1.Document.Blocks.LastBlock);

            ////添加图片
            //RichTextBoxTool.RichTextBoxAddImageCheck(RichTextBox1);

            //还原插入位置
            RichTextBox1.CaretPosition = textPointer;
        }

        /// <summary>
        /// 显示序号
        /// </summary>
        private void ShwoNumber()
        {
            System.Windows.Documents.List listx = new System.Windows.Documents.List();
            listx.Margin = new Thickness() { Left = 3 };
            // Set the space between the markers and list content to 25 DIP.
            listx.MarkerOffset = 5;
            // Use uppercase Roman numerals.
            listx.MarkerStyle = TextMarkerStyle.Decimal;
            // Start list numbering at 5.
            listx.StartIndex = 1;
            // Create the list items that will go into the list.
            System.Windows.Documents.ListItem liV = new System.Windows.Documents.ListItem(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run("")));
            // Finally, add the list items to the list.
            listx.ListItems.Add(liV);

            if (RichTextBox1.Document.Blocks.Count == 2)
                RichTextBox1.Document.Blocks.Remove(RichTextBox1.Document.Blocks.LastBlock);

            RichTextBox1.Document.Blocks.Add(listx);

            RichTextBox1.Focus();
            RichTextBox1.CaretPosition = this.RichTextBox1.Document.ContentEnd;
        }

        /// <summary>
        /// 设置提醒-手动设置
        /// </summary>
        private void SetRemind()
        {
            string resutStr = RichTextBoxTool.StringFromRichTextBox(RichTextBox1);
            string[] strs = resutStr.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (strs.Length == 0)
                resutStr = "";

            WindowRemind windowRemind = new WindowRemind(resutStr, RemindDateTime, _cancellationTokenSource);
            windowRemind.RemindDateTime = RemindDateTime;
            windowRemind.RemindAction += RemindAction;

            windowRemind.ShowDialog();
            if (windowRemind.IsSave)
                RemindDateTime = windowRemind.RemindDateTime;

        }

        /// <summary>
        /// 设置提醒-自动设置
        /// </summary>
        /// <param name="newDateTime"></param>
        private void SetRemind(DateTime newDateTime)
        {
            if (newDateTime <= DateTime.Now)
            {
                WindowMessage2 windowMessage = new WindowMessage2("朕想静静", "你退下吧");
                windowMessage.Text = string.Format("陛下，陛下，你设置的提醒时间{0}已经过去了！备注是：{1}", newDateTime, Caption);
                windowMessage.Title = "陛下！陛下";
                windowMessage.ShowDialog();

                RemindDateTime = null;
                return;
            }

            RemindDateTime = newDateTime;
            WindowRemind windowRemind = new WindowRemind(Caption, newDateTime, _cancellationTokenSource);
            windowRemind.RemindDateTime = RemindDateTime;
            windowRemind.RemindAction += RemindAction;
            windowRemind.CreateRemind(newDateTime);

        }

        /// <summary>
        /// 设置提醒回调函数
        /// </summary>
        private void RemindAction()
        {

            if (Dispatcher.Thread != Thread.CurrentThread)
                this.Dispatcher.Invoke(new Action(() =>
                {
                    WindowTopmost = true;
                    Activate();
                    WindowTopmost = false;
                }));
            else
            {
                WindowTopmost = true;
                Activate();
                WindowTopmost = false;
            }

            SkinM oldSkinM = this.SkinM;

            System.Collections.Generic.List<SkinM> list = new SkinManage().Init();

            foreach (SkinM m in list)
            {
                this.SkinM = m;
                Thread.Sleep(300);
            }
            this.SkinM = oldSkinM;

            if (Dispatcher.Thread != Thread.CurrentThread)
                this.Dispatcher.Invoke(new Action(() => { RemindActionShowWindowMessage2(); }));
            else
                RemindActionShowWindowMessage2();
        }

        private void RemindActionShowWindowMessage2()
        {
            WindowMessage2 windowMessage = new WindowMessage2("朕知道了", "等朕5分钟");
            windowMessage.Text = "陛下，说好的让这个时间点提醒你干大事，快看，我做到了！你的备注是：" + Caption;
            windowMessage.Title = "陛下！陛下";
            windowMessage.ShowDialog();

            if (windowMessage.OperatingButton == "等朕5分钟")
                SetRemind(DateTime.Now.AddMinutes(5));
            if (windowMessage.OperatingButton == "朕知道了")
                RemindDateTime = null;
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

        #endregion


    }

}
