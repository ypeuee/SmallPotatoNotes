using Client.Notepad.Tools;
using System.Collections.Generic;
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

        /// <summary>
        /// 动态加载选择项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            NotepadState state;
            switch (((FrameworkElement)ListBox2.SelectedItem).ToolTip)
            {
                case "显示中便签":
                    state = NotepadState.Visible;
                    break;
                case "已隐藏便签":
                    state = NotepadState.Collapsed;
                    break;
                case "已删除便签":
                    state = NotepadState.Delete;
                    break;
                default:
                    state = NotepadState.Visible;
                    break;
            }

            List<string> list = new List<string>();

            foreach (var m in NotepadManage.WindowList.FindAll(m => m.WindowSettings.NotepadState == state))
            {
                list.Add(RichTextBoxTool.StringFromRichTextBox(m.RichTextBox1));
            }

            ListBox1.ItemsSource = list;
        }
    }
}
