using System;
using System.Runtime.Serialization;

namespace YpeuEe.PublicModel.Message
{
    /// <summary>
    /// 用户反馈
    /// </summary>
    [DataContract]
    public class MFeedback
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        public string Content { get; set; }

        /// <summary>
        /// 状态（1、正常 2、禁用）
        /// </summary>
        [DataMember]
        public Byte Status { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 删除日期
        /// </summary>
        [DataMember]
        public DateTime DeleteDate { get; set; }
    }
}