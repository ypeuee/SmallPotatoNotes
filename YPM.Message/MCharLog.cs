using System;
using System.Runtime.Serialization;

namespace YpeuEe.PublicModel.Message
{
    [DataContract]
    public class MCharLog
    {
        ///<summary>
        ///主键Id
        ///</summary>
        [DataMember]
        public Int64? Id { get; set; }

        ///<summary>
        ///发送人
        ///</summary>
        [DataMember]
        public MUserInfo SendUserInfo { get; set; }
         

        ///<summary>
        ///接收人
        ///</summary>
        [DataMember]
        public MUserInfo ReceiveUserInfo { get; set; }

      
        ///<summary>
        ///信息
        ///</summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [DataMember]
        public DateTime? SendDate { get; set; }

        ///<summary>
        ///创建日期
        ///</summary>
        [DataMember]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 收到日期
        /// </summary>
        [DataMember]
        public DateTime? ReceiceDate { get; set; }


        ///<summary>
        /// 日志状态
        ///</summary>
        [DataMember]
        public int? StatusId { get; set; }

        ///<summary>
        /// 信息标识
        ///</summary>
        [DataMember]
        public Guid? MessageFlag { get; set; }
    }
}
