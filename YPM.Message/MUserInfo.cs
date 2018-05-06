using System;
using System.Runtime.Serialization;

namespace YpeuEe.PublicModel.Message
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [DataContract]
    public class MUserInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 性名
        /// </summary>
        [DataMember]
        public string Name { get; set; }
  
        /// <summary>
        /// 账号
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        public string Password { get; set; }
  
        /// <summary>
        /// 信息
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DataMember]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        public string Email { get; set; }
         
        ///<summary>
        ///性别（1男，0女）
        ///</summary>          
        [DataMember]
        public bool? Sex { get; set; }

        ///<summary>
        ///头像
        ///</summary>          
        [DataMember]
        public string HeadImgUrl { get; set; }

        ///<summary>
        ///省份
        ///</summary>          
        [DataMember]
        public string Province { get; set; }

        ///<summary>
        ///城市
        ///</summary>          
        [DataMember]
        public string City { get; set; }

        ///<summary>
        ///状态（1、正常 2、禁用）
        ///</summary>          
        [DataMember]
        public byte? Status { get; set; }

        ///<summary>
        ///创建日期
        ///</summary>          
        [DataMember]
        public DateTime CreateDate { get; set; }
     
        /// <summary>
        /// 类型0游客 1用户
        /// </summary>
        [DataMember]
        public byte Type { get; set; }
    }
}
