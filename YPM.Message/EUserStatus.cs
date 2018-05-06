using System.Runtime.Serialization;

namespace YpeuEe.PublicModel.Message
{
    /// <summary>
    /// 用户登录状态
    /// </summary>
    [DataContract]
    public enum EUserStatus
    {
        /// <summary>
        /// 在线
        /// </summary>
        [EnumMember]
        Online = 0,
        /// <summary>
        /// 忙碌
        /// </summary>
        [EnumMember]
        Busy = 1,
        /// <summary>
        /// 离开
        /// </summary>
        [EnumMember]
        GoAway = 2,
        /// <summary>
        /// 隐身
        /// </summary>
        [EnumMember]
        Stealth = 3,
        /// <summary>
        /// 离线
        /// </summary>
        [EnumMember]
        Offline = 4
    }

}
