using System;
using System.Runtime.Serialization;

namespace YpeuEe.PublicModel.Message
{
   
    /// <summary>
    /// 信息内容
    /// </summary>
    [DataContract]
    public class MMessage
    {
        /// <summary>
        /// 信息的ID
        /// </summary>
         //{Key}
        [DataMember]
        public Int64 Id { get; set; }

        [DataMember]
        private string _message;

        /// <summary>
        /// 信息内容
        /// </summary>
        [DataMember]
        public string Message
        {
            get { return _message; }
            set
            {
                if (value.Length > 10000)
                {
                    _message = value.Substring(0, 10000);
                    return;
                }
                _message = value;
            }
        }

        /////// <summary>
        /////// 信息内容
        /////// </summary>
        //[Required]
        //[StringLength(4, ErrorMessage = "The ThumbnailPhotoFileName value cannot exceed 4 characters. ")]
        //[DataMember]
        //public string Message { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [DataMember]
        public DateTime SendDate { get; set; }

        /// <summary>
        /// 信息级别
        /// </summary>
        [DataMember]
        public EMessageLevel MessageLevel { get; set; }

        /// <summary>
        /// 信息标识
        /// </summary>
        [DataMember]
        public Guid? MessageFlag { get; set; }

        /// <summary>
        /// 信息状态 1未发送 2已发送 3已确认
        /// </summary>
        [DataMember]
        public int StatusId { get; set; }
    }
    
}
