using Client.Notepad.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Notepad
{
    public class NotepadManage : BaseClass
    {
        #region 构造与属性

        private static List<WindowNotepad> _window1s;
        /// <summary>
        /// 所有便签缓存
        /// </summary>
        public static List<WindowNotepad> WindowList
        {
            get
            {
                if (_window1s == null)
                    _window1s = new List<WindowNotepad>();
                return _window1s;
            }
            set { _window1s = value; }
        }

        private static SystemSettingM _systemSetting;

        /// <summary>
        /// 系统设置
        /// </summary>
        public static SystemSettingM SystemSetting
        {
            get
            {
                if (_systemSetting == null)
                {
                    WindowSettingsAllM windowSettingsAll = ReadSetings();
                    if (windowSettingsAll != null && windowSettingsAll.SystemSetting != null)
                    {
                        _systemSetting = windowSettingsAll.SystemSetting;
                    }
                    else
                    {
                        _systemSetting = new SystemSettingM();
                        _systemSetting.ShowInTaskbar = true;
                    }
                }
                return _systemSetting;
            }
            internal set { _systemSetting = value; }
        }
        #endregion

        #region 创建便签

        /// <summary>
        /// 创建便签
        /// </summary>
        public static void CreateNotepad()
        {
            CreateNotepad(string.Empty, GetNewIndex());
        }

        /// <summary>
        /// 创建便签
        /// </summary>
        public static void CreateNotepad(WindowNotepad oldNotepad)
        {
            CreateNotepad(string.Empty, GetNewIndex(), oldNotepad);
        }


        /// <summary>
        /// 创建便签
        /// </summary>
        /// <param name="path">缓存路径</param>
        /// <param name="imdex"></param>
        public static void CreateNotepad(string path, int imdex)
        {
            CreateNotepad(path, imdex, null);
        }

        /// <summary>
        /// 创建便签
        /// </summary>
        /// <param name="path">缓存路径</param>
        /// <param name="imdex"></param>
        /// <param name="oldNotepad"></param>
        public static void CreateNotepad(string path, int imdex, WindowNotepad oldNotepad)
        {
            if (WindowList.Count >= 15)
            {
                MessageBox.Show("便签数据日经达到最大值15个！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (WindowList.Exists(t => t.CacheFileName == path))
                return;

            WindowNotepad window = null;
            WindowSettingsAllM windowSettingsAll = ReadSetings();
            string id = Path.GetFileNameWithoutExtension(path);
            WindowSettingsM settings = windowSettingsAll?.WindowSettingses.Find(t => t.ID == id);
            if (settings == null)
            {
                settings = GetDefaultWindowSettingsM(id);
            }

            window = new WindowNotepad(settings);
            SetTop(window, oldNotepad);
            window.Show();
            WindowList.Add(window);
            ShowWindowListCount++;
            WindowListCount++;
        }

        /// <summary>
        /// 创建便签
        /// </summary>
        /// <param name="path">缓存路径</param>
        /// <param name="settingses"></param>
        public static void CreateNotepad(string path, List<WindowSettingsM> settingses)
        {
            if (WindowList.Exists(t => t.CacheFileName == path))
                return;

            WindowNotepad window = null;

            string id = Path.GetFileNameWithoutExtension(path);
           
            WindowSettingsM settings = settingses.Find(t => t.ID == id);
            if (settings == null)
                settings = GetDefaultWindowSettingsM(id);

            window = new WindowNotepad(settings);
            window.Show();
            WindowList.Add(window);
            ShowWindowListCount++;
            WindowListCount++;
        }

        /// <summary>
        /// 获取新索引
        /// </summary>
        /// <returns></returns>
        public static int GetNewIndex()
        {
            int index = 0;
            foreach (WindowNotepad window in WindowList)
            {
                if (window.WindowSettings.Index > index)
                    index = window.WindowSettings.Index;
            }
            return index + 1;
        }

        public static WindowSettingsM GetDefaultWindowSettingsM(string id)
        {
            WindowSettingsM m = new WindowSettingsM();
            m.Title = SystemCommon.SystemName;
            if (string.IsNullOrEmpty(id))
                m.ID = RichTextBoxTool.PathNewCacheFileName;
            else
                m.ID = Path.GetFileNameWithoutExtension(id);
            m.Width = 250;
            m.Height = 300;
            m.TitleBottomFontSize = 14;
            m.TitleBottomHeight = 14;
            m.Opacity = 1;


            return m;
        }

        #endregion

        #region 设置新窗体位置

        /// <summary>
        /// 设置新窗体位置
        /// </summary>
        /// <param name="newNotepad">新窗体</param>
        /// <param name="oldNotepad">老窗体</param>
        /// <param name="isContinuous">是否连续添加</param>
        private static void SetTop(WindowNotepad newNotepad, WindowNotepad oldNotepad, bool isContinuous = false)
        {
            if (oldNotepad == null) return;

            //如果左边位置可放一个新窗体
            if (oldNotepad.Left > newNotepad.Width)
            {
                newNotepad.Left = oldNotepad.Left - newNotepad.Width - 5;
            }
            else
            {
                if (isContinuous)
                    newNotepad.Top = newNotepad.Top + 30;

                newNotepad.Left = oldNotepad.Left + newNotepad.Width + 5;
            }


            ContinuousCheck(newNotepad);
        }



        /// <summary>
        /// 位置检测 新位置是否已经有便笺
        /// </summary>
        /// <param name="newNotepad"></param>
        private static void ContinuousCheck(WindowNotepad newNotepad)
        {
            foreach (WindowNotepad notepad in WindowList)
            {
                if (!notepad.Equals(newNotepad) && notepad.Top == newNotepad.Top && notepad.Left == newNotepad.Left)
                {
                    SetTop(newNotepad, notepad, true);
                }
            }
        }

        #endregion

        #region 移除便签
        /// <summary>
        /// 显示的便签数不钮扣隐藏
        /// </summary>
        public static int ShowWindowListCount { get; set; }

        /// <summary>
        /// 便签总数，包括已经隐藏的，与现在显示的
        /// </summary>
        public static int WindowListCount { get; set; }

        /// <summary>
        /// 移除便签
        /// </summary>
        /// <param name="window"></param>
        public static void RemoveNotepad(WindowNotepad window)
        {
            if (WindowList.Contains(window))
            {
                WindowList.Remove(window);
            }
        }

        #endregion

        #region 便签操作

        /// <summary>
        /// 系统 激活所有的便签true;
        /// </summary>
        private static bool _isActivatedNotepad = false;

        /// <summary>
        /// 重新排序
        /// </summary>
        /// <param name="index"></param>
        public static void SetIndexOrderBy(int index)
        {
            if (_isActivatedNotepad == false)
            {
                foreach (WindowNotepad w in WindowList.OrderBy(i => i.WindowSettings.Index))
                {
                    if (w.WindowSettings.Index > index)
                    {
                        w.WindowSettings.Index--;
                    }
                    else if (w.WindowSettings.Index == index)
                    {
                        w.WindowSettings.Index = WindowList.Count;
                    }
                    //w.Caption = w.Index.ToString();

                }
            }
        }

        /// <summary>
        /// 打开所有便签
        /// </summary>
        public static void OpenAllNotepad()
        {
            _isActivatedNotepad = true;
            List<string> files = new List<string>(Directory.GetFiles(RichTextBoxTool.PathCacheVisible, SystemCommon.SearchExtensionName));

            //如果没有便签，则打开一个空的。
            if (files.Count == 0)
            {
                CreateNotepad();
                return;
            }

            WindowSettingsAllM windowSettingsAll = ReadSetings();

            foreach (string s in files)
            {
                CreateNotepad(Path.GetFileNameWithoutExtension(s), windowSettingsAll?.WindowSettingses);
            }
            _isActivatedNotepad = false;

            // ActivatedNotepad();
        }

        /// <summary>
        /// 激活所有的便签
        /// </summary>
        public static void ActivatedNotepad()
        {
            _isActivatedNotepad = true;
            //升序  
            foreach (WindowNotepad window in WindowList.OrderBy(s => s.WindowSettings.Index))
            {
                //window.Topmost = true;
                window.Activate();
                //window.Topmost = false;
                //Debug.WriteLine(window.Caption);
                //window.WindowsTool.ShowWindow(window);
            }
            _isActivatedNotepad = false;
        }

        #endregion

        #region 便签设置缓存

        /// <summary>
        /// 保存所有便签设置
        /// </summary>
        public static void SaveSetings(WindowNotepad windowNotepad = null)
        {
            WindowSettingsAllM windowSettingsAll = ReadSetings();
            if (windowSettingsAll == null)
                windowSettingsAll = new WindowSettingsAllM();
            if (windowSettingsAll.WindowSettingses == null)
                windowSettingsAll.WindowSettingses = new List<WindowSettingsM>();

            windowSettingsAll.SystemSetting = SystemSetting;

            List<WindowNotepad> list;
            if (windowNotepad != null)
                list = WindowList.FindAll(m => m.WindowSettings.ID == windowNotepad.WindowSettings.ID);
            else
                list = WindowList;
            foreach (WindowNotepad notepad in list)
            {
                notepad.WindowSettings.Top = notepad.Top;
                notepad.WindowSettings.Left = notepad.Left;
                notepad.WindowSettings.BackColorName = notepad.SkinM.Name;
                notepad.WindowSettings.Width = notepad.Width;
                notepad.WindowSettings.Height = notepad.Height;

                //替换为新设置
                int index = windowSettingsAll.WindowSettingses.FindIndex(t => t.ID == notepad.WindowSettings.ID);
                if (index > 0)
                {
                    windowSettingsAll.WindowSettingses.RemoveAt(index);
                }

                windowSettingsAll.WindowSettingses.Add(notepad.WindowSettings);
            }

            XMLSerializer.Serializer(windowSettingsAll);
        }

        /// <summary>
        /// 读取所有便签设置
        /// </summary>
        public static WindowSettingsAllM ReadSetings()
        {
            return XMLSerializer.Deserialize<WindowSettingsAllM>();
        }

        /// <summary>
        /// 设置便签
        /// </summary>
        /// <param name="window"></param>
        public static void SetWindowNotepad(WindowNotepad window)
        {
            WindowSettingsAllM windowSettingsAll = ReadSetings();
            if (windowSettingsAll == null) return;
            //if (_systemSetting == null && windowSettingsAll.SystemSetting != null)
            //    _systemSetting = windowSettingsAll.SystemSetting;
            WindowSettingsM settings = windowSettingsAll.WindowSettingses.Find(t => t.ID == window.WindowSettings.ID);
            if (settings != null)
            {
                window.WindowSettings = settings;
                window.SkinM = new SkinManage().GetSkin(settings.BackColorName);
            }

        }

        #endregion

        #region 时时监控是否有新导入的文件

        /// <summary>
        /// 打开导入的文件
        /// </summary>
        public static void OpenImputFileTask()
        {
            Task t1 = new Task(() =>
            {
                do
                {
                    Thread.Sleep(1000);
                    try
                    {
                        OpenImputFIle();
                    }
                    catch (Exception e)
                    {
                        ToolLogs.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "自动打开文件", e);
                    }


                } while (!SystemCommon.IsExitSystem);

            });
            t1.Start();
        }

        public static void OpenImputFIle()
        {
            //检测缓存文件数量是否有变化
            if (Directory.GetFiles(RichTextBoxTool.PathCache, SystemCommon.SearchExtensionName).Length > NotepadManage.WindowListCount)
            {
                //查找新导入文件并打开
                //DirectoryInfo info = new DirectoryInfo(RichTextBoxTool.PathCache);
                foreach (string s in Directory.GetFiles(RichTextBoxTool.PathCache, SystemCommon.SearchExtensionName))
                {
                    var v = WindowList.Find(m => m.CacheFileName == s);

                    if (v == null)
                    {

                        if (App.Current.Dispatcher.Thread != Thread.CurrentThread)
                        {
                            App.Current.Dispatcher.Invoke((Action)(() =>
                            {

                                CreateNotepad(Path.GetFileNameWithoutExtension(s), GetNewIndex());
                            }));
                        }
                        else
                        {

                            CreateNotepad(Path.GetFileNameWithoutExtension(s), GetNewIndex());
                        }

                    }
                }
            }

        }


        #endregion
    }
}