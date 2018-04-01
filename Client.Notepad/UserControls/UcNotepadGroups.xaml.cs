using Client.Notepad.Tools;
using System.Collections.Generic;
using System.IO;
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
                    BtnCollapsed.IsEnabled = true;
                    break;
                case "已隐藏便签":
                    state = NotepadState.Collapsed;
                    BtnVisible.IsEnabled = true;
                    break;
                case "已删除便签":
                    state = NotepadState.Delete;
                    BtnVisible.IsEnabled = true;
                    BtnCollapsed.IsEnabled = true;
                    break;
                default:
                    state = NotepadState.Visible;
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

            foreach (var m in NotepadManage.WindowList.FindAll(m => m.WindowSettings.NotepadState == state))
            {
                list.Add(new ListBoxItemM() { ID = m.CacheFileName, Text = RichTextBoxTool.StringFromRichTextBox(m.RichTextBox1) });
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
            var v = NotepadManage.WindowList.Find(m => m.CacheFileName == model.ID);
            v.WindowSettings.NotepadState = notepadState;
            v.Visibility = notepadState == NotepadState.Visible
                ? Visibility.Visible
                : Visibility.Collapsed;

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
            SetNotePad(NotepadState.Visible);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (ListBox1.SelectedItem == null)
            {
                return;
            }
            var model = (ListBoxItemM)ListBox1.SelectedItem;
            var v = NotepadManage.WindowList.Find(m => m.CacheFileName == model.ID);
            if (v.WindowSettings.NotepadState == NotepadState.Delete)
            {
                v.Close();

                NotepadManage.RemoveNotepad(v);

                if (File.Exists(v.CacheFileName))
                    File.Delete(v.CacheFileName);

                BingListBox1();
                //NotepadManage.WindowListCount--;
                return;
            }


            SetNotePad(NotepadState.Delete);
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCollapsed_OnClick(object sender, RoutedEventArgs e)
        {
            SetNotePad(NotepadState.Collapsed);
        }

        public class ListBoxItemM
        {
            public string ID { get; set; }

            public string Text { get; set; }
        }
    }
}
