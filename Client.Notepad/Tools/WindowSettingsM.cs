using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;

namespace Client.Notepad.Tools
{
    /// <summary>
    /// 所有设置对象
    /// </summary>
    [Serializable]
    public class WindowSettingsAllM
    {
        public List<WindowSettingsM> WindowSettingses { get; set; }

        /// <summary>
        /// 系统设置
        /// </summary>
        public SystemSettingM SystemSetting { get; set; }
    }

    /// <summary>
    /// 系统设置对象
    /// </summary>
    [Serializable]
    public class SystemSettingM
    {
        /// <summary>
        /// 获取或设置一个指示窗口是否具有任务栏按钮的值。这是一个依赖项属性。
        /// </summary>
        public bool ShowInTaskbar { get; set; }

    }

    /// <summary>
    /// 窗体设置对象
    /// </summary>
    [Serializable]
    public class WindowSettingsM
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 显示的索引
        /// </summary>
        [DefaultSettingValue("0")]
        public int Index { get; set; }

        /// <summary>
        /// 标题
        /// </summary>

        [DefaultSettingValue("")]
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置窗口左边缘相对于桌面的位置。
        /// </summary>
        [DefaultSettingValue("10")]
        public double Left { get; set; }

        //
        /// <summary>
        /// 获取或设置窗口上边缘相对于桌面的位置。
        /// </summary>
        [DefaultSettingValue("10")]
        public double Top { get; set; }

        /// <summary>
        /// 获取或设置此 System.Windows.Size 实例的 System.Windows.Size.Width。
        /// </summary>
        [DefaultSettingValue("200")]
        public double Width { get; set; }

        /// <summary>
        /// 获取或设置此 System.Windows.Size 实例的 System.Windows.Size.Height。
        /// </summary>
        [DefaultSettingValue("200")]
        public double Height { get; set; }


        /// <summary>
        /// 背景颜色名称
        /// </summary>

        [DefaultSettingValue("红")]
        public string BackColorName { get; set; }


        /// <summary>
        /// 隐藏后标题的高
        /// </summary>
        [DefaultSettingValue("12")]
        public double TitleBottomHeight { get; set; }

        /// <summary>
        /// 隐藏后标题的字体的大小
        /// </summary>
        [DefaultSettingValue("14")]
        public double TitleBottomFontSize { get; set; }

        /// <summary>
        /// 不透明度
        /// </summary>
        [DefaultSettingValue("1")]
        public double Opacity { get; set; }

        /// <summary>
        /// 提醒的时间
        /// </summary>
        public DateTime? RemindDateTime { get; set; }

        /// <summary>
        /// true隐藏 false显示
        /// </summary>
        public NotepadState NotepadState { get; set; }
    }

    /// <summary>
    /// 便签的状态
    /// </summary>
    public enum NotepadState: byte
    {
        /// <summary>
        /// 显示元素
        /// </summary>
        Visible = 0,

        /// <summary>
        /// 不显示元素，但为元素保留布局空间。
        /// </summary>
        Hidden = 1,

        /// <summary>
        /// 不显示元素，且不为其保留布局空间。
        /// </summary>
        Collapsed = 2,

        /// <summary>
        /// 删除中，已经到回收站
        /// </summary>
        Delete=3
    }
}

