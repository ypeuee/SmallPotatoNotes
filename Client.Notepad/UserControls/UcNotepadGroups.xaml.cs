using Client.Notepad.Tools;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Notepad.UserControls
{
    /// <summary>
    /// UcNotepadGroups.xaml 的交互逻辑
    /// </summary>
    public partial class UcNotepadGroups : UserControl
    {
        public UcNotepadGroups()
        {
            InitializeComponent();
        }

        private void UcNotepadGroups_OnLoaded(object sender, RoutedEventArgs e)
        {

        }
        NotepadState state;
        private string _selectedPath;
        /// <summary>
        /// 动态加载选择项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnIsEnabled();

            switch (((FrameworkElement)ListBox2.SelectedItem).ToolTip)
            {
                case "显示中便签":
                    state = NotepadState.Visible;
                    _selectedPath = RichTextBoxTool.PathCacheVisible;
                    BtnCollapsed.IsEnabled = true;
                    break;
                case "已隐藏便签":
                    state = NotepadState.Hidden;
                    _selectedPath = RichTextBoxTool.PathCacheHidden;
                    BtnVisible.IsEnabled = true;
                    break;
                case "已删除便签":
                    state = NotepadState.Delete;
                    _selectedPath = RichTextBoxTool.PathCacheDelete;
                    BtnVisible.IsEnabled = true;
                    BtnCollapsed.IsEnabled = true;
                    break;
                default:
                    state = NotepadState.Visible;
                    _selectedPath = RichTextBoxTool.PathCacheVisible;
                    break;
            }

            BingListBox1();
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void BingListBox1()
        {
            List<ListBoxItemM> list = new List<ListBoxItemM>();
            List<string> files = new List<string>(Directory.GetFiles(_selectedPath, SystemCommon.SearchExtensionName));
            foreach (string f in files)
            {
                list.Add(new ListBoxItemM() { Name =Path.GetFileName( f), Text = RichTextBoxTool.StringFromFlowDocument(f) });
            }

            ListBox1.ItemsSource = list;
        }

        public void BtnIsEnabled()
        {
            BtnVisible.IsEnabled = false;
            //BtnDelete.IsEnabled = false;
            BtnCollapsed.IsEnabled = false;
        }

        /// <summary>
        /// 设置便签
        /// </summary>
        /// <param name="notepadState"></param>
        private void SetNotePad(NotepadState notepadState)
        {
            if (ListBox1.SelectedItem == null)
            {
                return;
            }
            var model = (ListBoxItemM)ListBox1.SelectedItem;

            NotepadManage.CreateNotepad();


            //  ListBox1.Items.Remove(ListBox1.SelectedItem);
            BingListBox1();
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnVisible_OnClick(object sender, RoutedEventArgs e)
        {
            if (ListBox1.SelectedItem == null)
            {
                return;
            }
            var model = (ListBoxItemM)ListBox1.SelectedItem;

            if(File.Exists(RichTextBoxTool.PathCacheVisible + model.Name))
                File.Delete(RichTextBoxTool.PathCacheVisible + model.Name);

            //移动到显示文件夹
            Move(_selectedPath+  model.Name, RichTextBoxTool.PathCacheVisible+model.Name);

            //打开
            NotepadManage.CreateNotepad(RichTextBoxTool.PathCacheVisible + model.Name, 0);

            BingListBox1();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            //保存所有便签设置
            NotepadManage.SaveSetings();


            if (ListBox1.SelectedItem == null)
            {
                return;
            }
            var model = (ListBoxItemM)ListBox1.SelectedItem;
           
            //如果已经删除状态，则直接物理删除
            if (state == NotepadState.Delete)
            {
              File.Delete(RichTextBoxTool.PathCacheDelete + model.Name);

                BingListBox1();
             
                return;
            }

            //逻辑删除

            //如果现状态为隐藏
            if (state == NotepadState.Hidden)
            {
                //移动
                Move(_selectedPath + model.Name, RichTextBoxTool.PathCacheDelete + model.Name);

                BingListBox1();
                return;
            }

            //如果现状态为显示，则先关闭。
            if (state == NotepadState.Visible)
            {
                var v = NotepadManage.WindowList.Find(m => m.CacheFileName == RichTextBoxTool.PathCacheVisible + model.Name);
                v?.Close();

                //移动
                Move(_selectedPath + model.Name, RichTextBoxTool.PathCacheDelete + model.Name);

                BingListBox1();
                return;
            }
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCollapsed_OnClick(object sender, RoutedEventArgs e)
        {
            //保存所有便签设置
            NotepadManage.SaveSetings();


            if (ListBox1.SelectedItem == null)
            {
                return;
            }
            var model = (ListBoxItemM)ListBox1.SelectedItem;

            //如果现状态为显示
            if (state == NotepadState.Visible)
            {
                var v = NotepadManage.WindowList.Find(m => m.CacheFileName == RichTextBoxTool.PathCacheVisible + model.Name);
                v?.Close();

                //移动
                Move(_selectedPath + model.Name, RichTextBoxTool.PathCacheHidden + model.Name);

                BingListBox1();
            }

            //如果已经删除状态，则直接物理删除
            if (state == NotepadState.Delete)
            {
                //移动
                Move(_selectedPath + model.Name, RichTextBoxTool.PathCacheHidden + model.Name);

                BingListBox1();
                return;
            }
             
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        public void Move(string sourceFileName, string destFileName)
        {
            if (File.Exists(destFileName))
                File.Delete(destFileName);

            //移动
           File. Move(sourceFileName, destFileName);
        }

        public class ListBoxItemM
        {
  
            public string Text { get; set; }

            public string Name { get; set; }
             
        }
    }
}
