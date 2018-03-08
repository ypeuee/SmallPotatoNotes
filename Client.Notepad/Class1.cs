using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Client.Notepad
{
  public  class Class1
    {
        public static void Text()
        {
            IDataObject dataObj = Clipboard.GetDataObject();


            bool b1;
            //
            // 摘要:
            //     指定 ANSI 文本数据格式。   123
            //
            // 返回结果:
            //     一个表示 ANSI 文本数据格式的字符串：“Text”。
            b1 = dataObj.GetDataPresent(DataFormats.Text);
            if (b1)
            {
                Console.WriteLine(DataFormats.Text);
            }
            //
            // 摘要:
            //     指定 Extensible Application Markup Language (XAML) 包数据格式。
            //
            // 返回结果:
            //     一个表示 XAML 数据格式的字符串：“XamlPackage”。
            b1 = dataObj.GetDataPresent(DataFormats.XamlPackage);
            if (b1)
            {
                Console.WriteLine(DataFormats.XamlPackage);
            }
            //
            // 摘要:
            //     指定 Extensible Application Markup Language (XAML) 数据格式。
            //
            // 返回结果:
            //     一个表示 XAML 数据格式的字符串：“Xaml”。
            b1 = dataObj.GetDataPresent(DataFormats.XamlPackage);
            if (b1)
            {
                Console.WriteLine(DataFormats.XamlPackage);
            }
            //
            // 摘要:
            //     指定封装任何类型的可序列化数据对象的数据格式。
            //
            // 返回结果:
            //     一个表示可序列化数据格式的字符串：“PersistentObject”。
            b1 = dataObj.GetDataPresent(DataFormats.Serializable);
            if (b1)
            {
                Console.WriteLine(DataFormats.Serializable);
            }
            //
            // 摘要:
            //     指定 common language runtime (CLR) 字符串类数据格式。
            //
            // 返回结果:
            //     一个表示 CLR 字符串类数据格式的字符串：“System.String”。
            b1 = dataObj.GetDataPresent(DataFormats.StringFormat);
            if (b1)
            {
                Console.WriteLine(DataFormats.StringFormat);
            }
            //
            // 摘要:
            //     指定以逗号分隔的值 (CSV) 数据格式。
            //
            // 返回结果:
            //     一个表示以逗号分隔的值数据格式的字符串：“CSV”。
            b1 = dataObj.GetDataPresent(DataFormats.CommaSeparatedValue);
            if (b1)
            {
                Console.WriteLine(DataFormats.CommaSeparatedValue);
            }
            //
            // 摘要:
            //     指定 Rich Text Format (RTF) 数据格式。
            //
            // 返回结果:
            //     一个表示 RTF 数据格式的字符串：“Rich Text Format”。
            b1 = dataObj.GetDataPresent(DataFormats.Rtf);
            if (b1)
            {
                Console.WriteLine(DataFormats.Rtf);
            }
            //
            // 摘要:
            //     指定 HTML 数据格式。
            //
            // 返回结果:
            //     一个表示 HTML 数据格式的字符串：“HTML Format”。
            b1 = dataObj.GetDataPresent(DataFormats.Html);
            if (b1)
            {
                Console.WriteLine(DataFormats.Html);
            }
            //
            // 摘要:
            //     指定 Windows 区域设置（区域性）数据格式。
            //
            // 返回结果:
            //     一个表示 Windows 区域设置格式的字符串：“Locale”。
            b1 = dataObj.GetDataPresent(DataFormats.Locale);
            if (b1)
            {
                Console.WriteLine(DataFormats.Locale);
            }
            //
            // 摘要:
            //     指定 Windows 文件放置格式。
            //
            // 返回结果:
            //     一个表示 Windows 文件放置格式的字符串：“FileDrop”。
            b1 = dataObj.GetDataPresent(DataFormats.FileDrop);
            if (b1)
            {
                Console.WriteLine(DataFormats.FileDrop);
            }
            //
            // 摘要:
            //     指定波形音频数据格式。
            //
            // 返回结果:
            //     一个表示波形音频格式的字符串：“WaveAudio”。
            b1 = dataObj.GetDataPresent(DataFormats.WaveAudio);
            if (b1)
            {
                Console.WriteLine(DataFormats.WaveAudio);
            }
            //
            // 摘要:
            //     指定 Resource Interchange File Format (RIFF) 音频数据格式。
            //
            // 返回结果:
            //     一个表示 RIFF 音频数据格式的字符串：“RiffAudio”。
            b1 = dataObj.GetDataPresent(DataFormats.Riff);
            if (b1)
            {
                Console.WriteLine(DataFormats.Riff);
            }
            //
            // 摘要:
            //     指定 Windows 调色板数据格式。
            //
            // 返回结果:
            //     一个表示 Windows 调色板数据格式的字符串：“Palette”。
            b1 = dataObj.GetDataPresent(DataFormats.Palette);
            if (b1)
            {
                Console.WriteLine(DataFormats.Palette);
            }
            //
            // 摘要:
            //     指定标准 Windows OEM 文本数据格式。
            //
            // 返回结果:
            //     一个表示 Windows OEM 文本数据格式的字符串：“OEMText”。
            b1 = dataObj.GetDataPresent(DataFormats.OemText);
            if (b1)
            {
                Console.WriteLine(DataFormats.OemText);
            }
            //
            // 摘要:
            //     指定 Tagged Image File Format (TIFF) 数据格式。
            //
            // 返回结果:
            //     一个表示 TIFF 数据格式的字符串：“TaggedImageFileFormat”。
            b1 = dataObj.GetDataPresent(DataFormats.Tiff);
            if (b1)
            {
                Console.WriteLine(DataFormats.Tiff);
            }
            //
            // 摘要:
            //     指定 Windows 数据交换格式 (DIF) 数据格式。
            //
            // 返回结果:
            //     一个表示 DIF 数据格式的字符串：“DataInterchangeFormat”。
            b1 = dataObj.GetDataPresent(DataFormats.Dif);
            if (b1)
            {
                Console.WriteLine(DataFormats.Dif);
            }
            //
            // 摘要:
            //     指定 Windows 符号链接数据格式。
            //
            // 返回结果:
            //     一个表示 Windows 符号链接数据格式的字符串：“SymbolicLink”。
            b1 = dataObj.GetDataPresent(DataFormats.SymbolicLink);
            if (b1)
            {
                Console.WriteLine(DataFormats.SymbolicLink);
            }
            //
            // 摘要:
            //     指定 Windows 图元文件图片数据格式。
            //
            // 返回结果:
            //     一个表示 Windows 图元文件图片数据格式的字符串：“MetaFilePict”。
            b1 = dataObj.GetDataPresent(DataFormats.MetafilePicture);
            if (b1)
            {
                Console.WriteLine(DataFormats.MetafilePicture);
            }
            //
            // 摘要:
            //     指定 Windows 增强型图元文件格式。
            //
            // 返回结果:
            //     一个表示 Windows 增强型图元文件格式的字符串：“EnhancedMetafile”。
            b1 = dataObj.GetDataPresent(DataFormats.EnhancedMetafile);
            if (b1)
            {
                Console.WriteLine(DataFormats.EnhancedMetafile);
            }
            //
            // 摘要:
            //     指定 Microsoft Windows 位图数据格式。
            //
            // 返回结果:
            //     一个表示 Windows 位图数据格式的字符串：“Bitmap”。
            b1 = dataObj.GetDataPresent(DataFormats.Bitmap);
            if (b1)
            {
                Console.WriteLine(DataFormats.Bitmap);
            }
            //
            // 摘要:
            //     指定 device-independent bitmap (DIB) 数据格式。
            //
            // 返回结果:
            //     一个表示 DIB 数据格式的字符串：“DeviceIndependentBitmap”。
            b1 = dataObj.GetDataPresent(DataFormats.Dib);
            if (b1)
            {
                Console.WriteLine(DataFormats.Dib);
            }
            //
            // 摘要:
            //     指定 Unicode 文本数据格式。
            //
            // 返回结果:
            //     一个表示 Unicode 文本数据格式的字符串：“UnicodeText”。
            b1 = dataObj.GetDataPresent(DataFormats.UnicodeText);
            if (b1)
            {
                Console.WriteLine(DataFormats.UnicodeText);
            }
            //
            // 摘要:
            //     指定 Windows 钢笔数据格式。
            //
            // 返回结果:
            //     一个表示 Windows 钢笔数据格式的字符串：“PenData”。
            b1 = dataObj.GetDataPresent(DataFormats.PenData);
            if (b1)
            {
                Console.WriteLine(DataFormats.PenData);
            }
            return;
        }
    }
}
