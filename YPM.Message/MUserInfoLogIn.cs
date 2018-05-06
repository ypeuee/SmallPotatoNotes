using System.Runtime.Serialization;

namespace YpeuEe.PublicModel.Message
{
    /// <summary>
    /// 主要用于登录
    /// 记录客户端信息如：MAC等 
    /// </summary>
    [DataContract]
    public class MUserInfoLogin
    {
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

    }
    /// <summary>
    /// 主要用于注册
    /// </summary>
    [DataContract]
    public class MUserInfoRegistered
    {
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
        /// 电脑ID
        /// </summary>
        [DataMember]
        public string ComputerId { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /////// <summary>
        /////// 验证码
        /////// </summary>
        ////[DataMember]
        ////public string VerificationCode { get; set; }
    }
}
