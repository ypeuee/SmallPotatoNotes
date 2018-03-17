using System;
using System.Windows;
using System.Windows.Controls;

namespace Client.Notepad.Tools
{
    public class WindowsTool
    {
        #region 构造与属性
        /// <summary>
        /// Windows工具类
        /// </summary>
        /// <param name="window">操作的窗体</param>
        public WindowsTool(WindowNotepad window)
        {
            Window = window;
        }

        /// <summary>
        /// 操作的窗体
        /// </summary>
        public WindowNotepad Window { get; set; }
        #endregion

        #region 鼠标左键拖动

        /// <summary>
        /// 鼠标左键拖动
        /// </summary>
        /// <param name="grid"></param>
        public void DragMove(Grid grid)
        {
            grid.MouseLeftButtonDown += (sender, e) =>
            {
                try
                {
                    Window.DragMove();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }

               //Window.WindowTopmost = Window.Top <= 21;
            };
        }

        #endregion

        #region 禁止win7以上的系统新增了功能--窗口拖到屏幕边缘自动最大化

        private ResizeMode _oldResizeMode;

        /// <summary>
        /// 禁止win7以上的系统新增了功能--窗口拖到屏幕边缘自动最大化
        /// </summary>
        public void SetNotMax(Grid grid)
        {
            //移动时禁止最大化PreviewMouseLeftButtonDown
            grid.MouseEnter += (sender, e) =>
            {
                Window.IsShosButton(true);
                _oldResizeMode = Window.ResizeMode;
                Window.ResizeMode = ResizeMode.NoResize;
            };

            //还原PreviewMouseLeftButtonUp
            grid.MouseLeave += (sender, e) =>
            {
                Window.IsShosButton(false);

                if (Window.ResizeMode == ResizeMode.NoResize)
                {
                    Window.ResizeMode = _oldResizeMode;
                }
            };

        }

        #endregion

        #region 放到顶部后，自动隐藏。

        /// <summary>
        /// true隐藏
        /// </summary>
        private bool _isHide;

        /// <summary>
        /// 自动显示
        /// 放到顶部后，自动隐藏。
        /// </summary>
        public void SetTopAutoShow(Grid gridTitleBottom, Grid gridTitle)
        {
            if (Window.Top < 0)
            {
                gridTitleBottom.Height = Window.WindowSettings.TitleBottomHeight;
                Window.WindowTopmost = true;
                _isHide = true;//隐藏

                //gridTitleBottom.BringToFront();//将控件放置所有控件最前端  
                //Canvas.SetZIndex(gridTitleBottom,0);

            }
            else
            {
                gridTitleBottom.Height = 0;
                _isHide = false;//不隐藏
            }

            //右击自动隐藏
            gridTitle.MouseRightButtonUp += (sender, e) =>
            {
                Hide(gridTitleBottom);
            };

            //鼠标进入窗口
            Window.MouseMove += (sender, e) =>
            {
                Show(gridTitleBottom);
            };

            //鼠标离开窗口
            Window.MouseLeave += (sender, e) =>
            {
                HideAuto(gridTitleBottom);
            };
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="gridTitleBottom"></param>
        public void Show(Grid gridTitleBottom)
        {
            if (Window.Top < 0)
            {
                gridTitleBottom.Height = 0;
                while (Window.Top < 0)
                {
                    Window.Top++;
                }
            }
        }

        /// <summary>
        /// 自动隐藏
        /// </summary>
        /// <param name="gridTitleBottom"></param>
        public void HideAuto(Grid gridTitleBottom)
        {
            if (Window.Top <= 21)
                Hide(gridTitleBottom);

            //Window.WindowTopmost = Window.Top <= 21;
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="gridTitleBottom"></param>
        public void Hide(Grid gridTitleBottom)
        {
            if (Window.Top >= -Window.ActualHeight + Window.WindowSettings.TitleBottomHeight)
            {
                gridTitleBottom.Height = Window.WindowSettings.TitleBottomHeight;

                if (!Window.WindowTopmost)
                    Window.WindowTopmost = true;


                while (Window.Top >= -Window.ActualHeight + Window.WindowSettings.TitleBottomHeight)
                {
                    Window.Top--;
                }
            }
          
        }

        #endregion

    }
}
