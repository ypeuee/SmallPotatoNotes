using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Client.Notepad
{
    /// <summary>
    /// 皮肤管理类
    /// </summary>
    public class SkinManage : BaseClass
    {
        public int DefaultSkinName { get { return 1; } }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public List<SkinM> Init()
        {
            List<SkinM> listSkinMs = new List<SkinM>(6);
            SkinM skinM;
            //黄
            skinM = new SkinM();
            skinM.Name = "黄";
            skinM.TitleColorS = new MyColour(255, 248, 247, 182);
            skinM.TitleColorE = new MyColour(255, 248, 247, 182);
            skinM.ContentColorS = new MyColour(255, 253, 253, 203);
            skinM.ContentColorE = new MyColour(255, 252, 249, 162);
            listSkinMs.Add(skinM);
            //红
            skinM = new SkinM();
            skinM.Name = "红";
            skinM.TitleColorS = new MyColour(255, 241, 195, 241);
            skinM.TitleColorE = new MyColour(255, 241, 195, 241);
            skinM.ContentColorS = new MyColour(255, 245, 209, 245);
            skinM.ContentColorE = new MyColour(255, 235, 174, 235);
            listSkinMs.Add(skinM);
            //紫
            skinM = new SkinM();
            skinM.Name = "紫";
            skinM.TitleColorS = new MyColour(255, 212, 205, 243);
            skinM.TitleColorE = new MyColour(255, 212, 205, 243);
            skinM.ContentColorS = new MyColour(255, 221, 217, 254);
            skinM.ContentColorE = new MyColour(255, 198, 184, 254);
            listSkinMs.Add(skinM);
            //蓝
            skinM = new SkinM();
            skinM.Name = "蓝";
            skinM.TitleColorS = new MyColour(255, 201, 236, 248);
            skinM.TitleColorE = new MyColour(255, 201, 236, 248);
            skinM.ContentColorS = new MyColour(255, 215, 242, 250);
            skinM.ContentColorE = new MyColour(255, 184, 218, 244);
            listSkinMs.Add(skinM);
            //绿
            skinM = new SkinM();
            skinM.Name = "绿";
            skinM.TitleColorS = new MyColour(255, 197, 247, 193);
            skinM.TitleColorE = new MyColour(255, 197, 247, 193);
            skinM.ContentColorS = new MyColour(150, 209, 254, 203);
            skinM.ContentColorE = new MyColour(255, 177, 232, 174);
            listSkinMs.Add(skinM);
            //白
            skinM = new SkinM();
            skinM.Name = "白";
            skinM.TitleColorS = new MyColour(255, 245, 245, 245);
            skinM.TitleColorE = new MyColour(255, 245, 245, 245);
            skinM.ContentColorS = new MyColour(150, 254, 254, 254);
            skinM.ContentColorE = new MyColour(255, 235, 235, 235);

            listSkinMs.Add(skinM);

            return listSkinMs;
        }

        /// <summary>
        /// 获取默认皮肤
        /// </summary>
        /// <returns></returns>
        public SkinM GetDefault()
        {
            List<SkinM> list = Init();

            return list[DefaultSkinName];
        }

        /// <summary>
        /// 获取随机皮肤
        /// </summary>
        /// <returns></returns>
        public SkinM GetRandom()
        {
            List<SkinM> list = Init();
            System.Random random = new Random();
            return list[random.Next(list.Count)];
        }


        /// <summary>
        /// 获取皮肤
        /// </summary>
        /// <param name="name">皮肤名称</param>
        /// <returns></returns>
        public SkinM GetSkin(string name)
        {
            List<SkinM> list = Init();
            SkinM skinM = list.Find(m => m.Name == name);

            if (skinM == null)
                skinM = GetDefault();

            return skinM;
        }



    }

    /// <summary>
    /// 皮肤类
    /// </summary>
    [Serializable()]
    public class SkinM
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标题颜色开始
        /// </summary>
        public MyColour TitleColorS { get; set; }

        public Color TitleS
        {
            get { return Color.FromArgb(TitleColorS.A, TitleColorS.R, TitleColorS.G, TitleColorS.B); }
        }


        /// <summary>
        /// 标题颜色结束
        /// </summary>
        public MyColour TitleColorE { get; set; }

        public Color TitleE
        {
            get { return Color.FromArgb(TitleColorE.A, TitleColorE.R, TitleColorE.G, TitleColorE.B); }
        }

        //private Color _contentColorS;

        //public Color ContentColorS
        //{
        //    get { return _contentColorS; }
        //    set { _contentColorS = value; }
        //}

        /// <summary>
        /// 内容颜色开始
        /// </summary>
        public MyColour ContentColorS { get; set; }

        public Color ContentS
        {
            get { return Color.FromArgb(ContentColorS.A, ContentColorS.R, ContentColorS.G, ContentColorS.B); }
        }

        //private Color _contentColorE;

        //public Color ContentColorE
        //{
        //    get { return _contentColorE; }
        //    set { _contentColorE = value; }
        //}

        /// <summary>
        /// 内容颜色结束
        /// </summary>
        public MyColour ContentColorE { get; set; }

        public Color ContentE
        {
            get { return Color.FromArgb(ContentColorE.A, ContentColorE.R, ContentColorE.G, ContentColorE.B); }
        }
    }

    /// <summary>
    /// 自定义RGBA
    /// </summary>
    public class MyColour
    {
        public MyColour() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public MyColour(byte a, byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        public byte A { get; set; }
    }

}
