using System;
using System.Configuration;
using System.Windows;
using System.Windows.Media;

namespace Client.Notepad
{
    /// <summary>
    /// Helper class used to save the settings in configuration file
    /// </summary>
    public class WindowApplicationSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string Title
        {
            get { return (string)this["Title"]; }
            set { this["Title"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("0, 0")]
        public Point WinLocation
        {
            get { return (Point)(this["WinLocation"]); }
            set { this["WinLocation"] = value; }
        }

        [UserScopedSetting()]
        public String FormText
        {
            get { return (String)this["FormText"]; }
            set { this["FormText"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("0, 0")]
        public Point FormLocation
        {
            get { return (Point)(this["FormLocation"]); }
            set { this["FormLocation"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("225, 200")]
        public Size FormSize
        {
            get { return (Size)this["FormSize"]; }
            set { this["FormSize"] = value; }
        }


        [UserScopedSetting()]
        [DefaultSettingValue("LightGray")]
        public Color FormBackColor
        {
            get { return (Color)this["FormBackColor"]; }
            set { this["FormBackColor"] = value; }
        }
    }
}