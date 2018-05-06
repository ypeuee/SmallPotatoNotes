using System.Runtime.Serialization;

namespace YpeuEe.PublicModel.Message
{

    [DataContract]
    public class PageModel
    {
        private int _page = 0;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int Page
        {
            get { return _page; }
            set { _page = value; }
        }

        private int _count = 10;

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }


        public string OrderBy { get; set; }


    }
}
