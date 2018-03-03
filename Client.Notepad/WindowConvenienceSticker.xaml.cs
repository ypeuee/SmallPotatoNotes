using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
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

namespace Client.Notepad
{
    /// <summary>
    /// WindowConvenienceSticker.xaml 的交互逻辑
    /// </summary>
    public partial class WindowConvenienceSticker : Window
    {
        public WindowConvenienceSticker()
            : this(string.Empty)
        {

        }

        public WindowConvenienceSticker(string cacheFileName)
        {
            InitializeComponent();


            CacheFileName = cacheFileName;

            if (string.IsNullOrEmpty(CacheFileName))
            {
                CacheFileName = RichTextBoxTool.PathNewCacheFileName;
            }
        }

        /// <summary>
        /// 缓存本地文件的名称
        /// </summary>
        public string CacheFileName { get; private set; }

        /// <summary>
        /// 加载已经缓存的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowConvenienceSticker_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(CacheFileName))
            {
                RichTextBoxTool.Read(RichTextBox1, CacheFileName);
            }
        }
        /// <summary>
        /// 关闭时缓存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowConvenienceSticker_OnClosing(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty( CacheFileName))
            {
                RichTextBoxTool.Writer(RichTextBox1, CacheFileName);
            }
        }

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowShowImage frm = new WindowShowImage(((System.Windows.Controls.Image)sender).Source);
            frm.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string xw = System.Windows.Markup.XamlWriter.Save(RichTextBox1.Document);
            MessageBox.Show(xw);

            System.IO.StringReader sr = new System.IO.StringReader(xw);
            System.Xml.XmlReader xr = System.Xml.XmlReader.Create(sr);
            RichTextBox1.Document = (FlowDocument)System.Windows.Markup.XamlReader.Load(xr);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //RichTextBoxEx.LoadFromRTF(RichTextBox1, s);

            var vvv = RichTextBox1.Selection;
            //vvv. is Paragraph
            foreach (Block block in RichTextBox1.Document.Blocks)
            {
                if (block is Paragraph)
                {
                    InlineCollection p =(block as Paragraph).Inlines;
                    InlineUIContainer u = p.FirstInline as InlineUIContainer;
                    var v = u.Child as System.Windows.Controls.Image;
                }

             
                //InlineUIContainer container = new InlineUIContainer(image);
                ////UIElement Child 
                //Paragraph paragraph = new Paragraph(container);
                ////InlineCollection Inlines 
            }

        }


        private void BtnAddImage_OnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = "*.png|*.jpg";
            ofd.Filter = "png file|*.png|jpg file|*.jpg";
            if (ofd.ShowDialog() == false)
            {
                return;
            }

            //添加图片
            RichTextBoxTool.RichTextBoxAddImage(RichTextBox1, ofd.FileName, image_MouseDown);
        }

    }


}
