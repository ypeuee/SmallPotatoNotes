using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Xml;

namespace Client.Notepad.Tools
{
    /// <summary>
    /// RichTextBox工具
    /// </summary>
    public class RichTextBoxTool
    {
        #region 构造与属性

        private static string _pathImage;
        /// <summary>
        /// 图片缓存路径
        /// </summary>
        public static string PathImage
        {
            get
            {
                if (string.IsNullOrEmpty(_pathImage))
                {
                    _pathImage = SystemCommon.Path + "Image\\"; //
                    if (!Directory.Exists(_pathImage))
                    {
                        Directory.CreateDirectory(_pathImage);
                    }
                }
                return _pathImage;
            }
        }

        private static string _pathCache;
        /// <summary>
        /// 缓存路径
        /// </summary>
        public static string PathCache
        {
            get
            {
                if (string.IsNullOrEmpty(_pathCache))
                {
                    _pathCache = SystemCommon.Path + "Cache\\"; //
                    if (!Directory.Exists(_pathCache))
                    {
                        Directory.CreateDirectory(_pathCache);
                    }
                }
                return _pathCache;
            }
        }

        private static string _pathCacheVisible;
        /// <summary>
        /// 缓存路径--显示
        /// </summary>
        public static string PathCacheVisible
        {
            get
            {
                if (string.IsNullOrEmpty(_pathCacheVisible))
                {
                    _pathCacheVisible = PathCache + "Visible\\"; //
                    if (!Directory.Exists(_pathCacheVisible))
                    {
                        Directory.CreateDirectory(_pathCacheVisible);
                    }
                }
                return _pathCacheVisible;
            }
        }

        private static string _pathCacheHidden;
        /// <summary>
        /// 缓存路径--隐藏
        /// </summary>
        public static string PathCacheHidden
        {
            get
            {
                if (string.IsNullOrEmpty(_pathCacheHidden))
                {
                    _pathCacheHidden = PathCache + "Hidden\\"; //
                    if (!Directory.Exists(_pathCacheHidden))
                    {
                        Directory.CreateDirectory(_pathCacheHidden);
                    }
                }
                return _pathCacheHidden;
            }
        }

        private static string _pathCacheDelete;
        /// <summary>
        /// 缓存路径--删除
        /// </summary>
        public static string PathCacheDelete
        {
            get
            {
                if (string.IsNullOrEmpty(_pathCacheDelete))
                {
                    _pathCacheDelete = PathCache + "Delete\\"; //
                    if (!Directory.Exists(_pathCacheDelete))
                    {
                        Directory.CreateDirectory(_pathCacheDelete);
                    }
                }
                return _pathCacheDelete;
            }
        }


        private static string _windowSetingFileName;

        public static string WindowSetingFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_windowSetingFileName))
                    _windowSetingFileName = "WindowSeting";

                return _windowSetingFileName;
            }
        }


        private static string _windowSetingPath;
        /// <summary>
        /// 窗体配置缓存路径(包括名称)
        /// </summary>
        public static string WindowSetingPath
        {
            get
            {
                if (string.IsNullOrEmpty(_windowSetingPath))
                {
                    _windowSetingPath = PathCache + WindowSetingFileName;
                }
                return _windowSetingPath;
            }
        }

        /// <summary>
        /// 新缓存名称
        /// </summary>
        public static string PathNewCacheFileName
        {
            get { return Guid.NewGuid().ToString(); }
        }

        #endregion

        #region RichTextBox中添加图片

        /// <summary>
        /// 把图片复制到Image目录，并返回新图片路径
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <returns></returns>
        public static string GetNewImageId(string sourcePath)
        {
            FileInfo info = new FileInfo(sourcePath);
            //新路径
            string newPath = PathImage;
            newPath = newPath + Guid.NewGuid() + "." + info.Extension;
            info.CopyTo(newPath);

            return newPath;
        }
        /// <summary>
        /// RichTextBox中添加图片
        /// </summary>
        /// <param name="textBox"></param>
        public static void RichTextBoxAddImageCheck(RichTextBox textBox)
        {
            //把图片复制到Image目录
            string newPaht = @"D:\HAO\MyProjects\Ypeuee.Message\Client\Client.Notepad\bin\Debug\Images\确认.png";

            BitmapImage bi = new BitmapImage(new Uri(newPaht));//new Uri(@"C:\Users\Administrator\Desktop\111.png"));
            Image image = new Image();
            //image.Stretch = System.Windows.Media.Stretch.Uniform;
            image.Width = 20;
            image.Height = 20;
            image.StretchDirection = StretchDirection.DownOnly;
            image.Source = bi;
            InlineUIContainer container = new InlineUIContainer(image);
            Paragraph paragraph = new Paragraph(container);
            textBox.Document.Blocks.Add(paragraph);
        }

        /// <summary>
        /// RichTextBox中添加图片
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="pathImage">图片路径</param>
        /// <param name="action"></param>
        public static void RichTextBoxAddImage(RichTextBox textBox, string pathImage, MouseButtonEventHandler action)
        {
            if (!IsMatchImageExtension(pathImage))
                return;

            //把图片复制到Image目录
            string newPaht = GetNewImageId(pathImage);

            BitmapImage bi = new BitmapImage(new Uri(newPaht));//new Uri(@"C:\Users\Administrator\Desktop\111.png"));
            Image image = new Image();
            //image.Stretch = System.Windows.Media.Stretch.Uniform;
            image.Width = 200;
            image.Height = 200;
            image.StretchDirection = StretchDirection.DownOnly;
            image.Source = bi;
            image.PreviewMouseLeftButtonUp += (sender, e) => { };
            image.MouseDown += new MouseButtonEventHandler(action);
            InlineUIContainer container = new InlineUIContainer(image);
            Paragraph paragraph = new Paragraph(container);
            textBox.Document.Blocks.Add(paragraph);
        }

        /// <summary>
        /// 验证是否是指定的格式
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsMatchImageExtension(string path)
        {
            List<string> list = new List<string>()
            {
                ".png",".jpg",".gif"
            };

            FileInfo fileInfo = new FileInfo(path);

            string s = list.Find(e => e == fileInfo.Extension);
            return s != null;
        }

        #endregion

        #region 读写本地文件

        /// <summary>
        /// 写入到本地文件
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="pathFile"></param>
        public static void Writer(RichTextBox textBox, string pathFile)
        {
            string xw = XamlWriter.Save(textBox.Document);
            File.WriteAllText(pathFile, xw, Encoding.UTF8);
        }

        /// <summary>
        /// 从本地文件读取
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="pathFile"></param>
        public static void Read(RichTextBox textBox, string pathFile)
        {
            string xw = File.ReadAllText(pathFile, Encoding.UTF8);

            StringReader sr = new StringReader(xw);
            XmlReader xr = XmlReader.Create(sr);
            textBox.Document = (FlowDocument)XamlReader.Load(xr);
            //设置光标到最后位置
            textBox.CaretPosition = textBox.Document.ContentEnd;
        }

        /// <summary>
        /// 从本地文件读取
        /// </summary>
        /// <param name="pathFile"></param>
        public static FlowDocument Read( string pathFile)
        {
            string xw = File.ReadAllText(pathFile, Encoding.UTF8);

            StringReader sr = new StringReader(xw);
            XmlReader xr = XmlReader.Create(sr);
           return (FlowDocument)XamlReader.Load(xr);
        }

        /// <summary>
        /// 从本地文件读取
        /// </summary>
        /// <param name="pathFile"></param>
        public static string StringFromFlowDocument(string pathFile)
        {
            FlowDocument flow = Read(pathFile);

            TextRange textRange = new TextRange(
               // TextPointer to the start of content in the RichTextBox.
                flow.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                flow.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }

        /// <summary>
        /// 添加扩展名
        /// todo:添加后缀过度代码
        /// </summary>
        public static void AddExtenstion()
        {
            DirectoryInfo directory = new DirectoryInfo(RichTextBoxTool.PathCache);
            foreach (FileInfo f in directory.GetFiles())
            {
                if (f.Name == WindowSetingFileName)
                    continue;
                if (f.Extension == string.Empty)
                {
                    if (File.Exists(f.FullName + SystemCommon.Extension))
                    {
                        continue;
                    }
                    f.MoveTo(Path.Combine(f.FullName + SystemCommon.Extension));
                }
            }
        }

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="rtb"></param>
        /// <returns></returns>
        public static string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }

        #endregion

        #region 复制、剪切、粘贴、全选、删除

        //private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    Paste();
        //    e.Handled = true;
        //}


        /// <summary>
        /// 去格式粘贴
        /// </summary>
        /// <param name="richTextBox"></param>
        public static void Paste(RichTextBox richTextBox)
        {
            //Get Unicode Text
            string paste = Clipboard.GetText();
            Clipboard.Clear();
            Clipboard.SetText(paste);
            richTextBox.Paste();
            //e.Handled = true;
        }


        //private void tbClob_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Control && e.KeyCode == Keys.V)
        //    {
        //        IDataObject dataObj = Clipboard.GetDataObject();
        //        if (dataObj.GetDataPresent(DataFormats.StringFormat))
        //        {
        //            e.Handled = true; //去掉格式文本的格式 
        //            var txt = (string)Clipboard.GetData(DataFormats.StringFormat);
        //            Clipboard.Clear();
        //            Clipboard.SetData(DataFormats.StringFormat, txt);
        //            tbClob.Paste();
        //        }
        //    }
        //}

        //private void tbTemplate_KeyDown(object sender, KeyEventArgs e)
        //{
        //    //防止ctrl+v粘贴有格式的文本进来 
        //    if (e.Control && e.KeyCode == Keys.V)
        //    {
        //        e.Handled = true;
        //        //这句是关键，不然你会发现粘贴了两次 
        //        v.miPaste.PerformClick();
        //    }
        //}

        //private void miPaste_Click(object sender, EventArgs e)
        //{
        //    v.tbTemplate.Paste(DataFormats.GetFormat(TextDataFormat.UnicodeText.ToString()));
        //}

        #endregion

        //其它方法------------------------------------

        #region 其它方法

        /// <summary>
        /// 从文件中读出纯文本文件后放进RichTextBox或直接将文本放进RichTextBox中
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="filename"></param>
        private void LoadTextFile(RichTextBox richTextBox, string filename)
        {
            richTextBox.Document.Blocks.Clear();
            using (StreamReader streamReader = File.OpenText(filename))
            {
                Paragraph paragraph = new Paragraph();
                paragraph.DataContext = streamReader.ReadToEnd();
                richTextBox.Document.Blocks.Add(paragraph);
            }
        }
        /// <summary>
        /// 加载文本
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="txtContent"></param>
        private void LoadText(RichTextBox richTextBox, string txtContent)
        {
            richTextBox.Document.Blocks.Clear();
            Paragraph paragraph = new Paragraph();
            paragraph.DataContext = txtContent;
            richTextBox.Document.Blocks.Add(paragraph);
        }

        /// <summary>
        /// 取得指定RichTextBox的内容
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <returns></returns>
        private string GetText(RichTextBox richTextBox)
        {
            TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            return textRange.Text;
        }
        /// <summary>
        /// 将RTF (rich text format)放到RichTextBox中：
        /// </summary>
        /// <param name="rtf"></param>
        /// <param name="richTextBox"></param>
        private static void LoadRTF(string rtf, RichTextBox richTextBox)
        {
            if (string.IsNullOrEmpty(rtf))
            {
                throw new ArgumentNullException();
            }
            TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            using (MemoryStream rtfMemoryStream = new MemoryStream())
            {
                using (StreamWriter rtfStreamWriter = new StreamWriter(rtfMemoryStream))
                {
                    rtfStreamWriter.Write(rtf);
                    rtfStreamWriter.Flush();
                    rtfMemoryStream.Seek(0, SeekOrigin.Begin);
                    //Load the MemoryStream into TextRange ranging from start to end of RichTextBox.
                    textRange.Load(rtfMemoryStream, DataFormats.Rtf);
                }
            }
        }
        /// <summary>
        /// 将文件中的内容加载为RichTextBox的内容
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="richTextBox"></param>
        private static void LoadFile(string filename, RichTextBox richTextBox)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException();
            }
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException();
            }
            using (FileStream stream = File.OpenRead(filename))
            {
                TextRange documentTextRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                string dataFormat = DataFormats.Text;
                string ext = Path.GetExtension(filename);
                if (String.Compare(ext, ".xaml", true) == 0)
                {
                    dataFormat = DataFormats.Xaml;
                }
                else if (String.Compare(ext, ".rtf", true) == 0)
                {
                    dataFormat = DataFormats.Rtf;
                }
                documentTextRange.Load(stream, dataFormat);
            }
        }
        /// <summary>
        /// 将RichTextBox的内容保存为文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="richTextBox"></param>
        private static void SaveFile(string filename, RichTextBox richTextBox)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException();
            }
            using (FileStream stream = File.OpenWrite(filename))
            {
                TextRange documentTextRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                string dataFormat = DataFormats.Text;
                string ext = Path.GetExtension(filename);
                if (String.Compare(ext, ".xaml", true) == 0)
                {
                    dataFormat = DataFormats.Xaml;
                }
                else if (String.Compare(ext, ".rtf", true) == 0)
                {
                    dataFormat = DataFormats.Rtf;
                }
                documentTextRange.Save(stream, dataFormat);
            }
        }

        #endregion
    }
}
