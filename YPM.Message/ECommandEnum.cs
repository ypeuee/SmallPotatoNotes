using System.Runtime.Serialization;

namespace YpeuEe.PublicModel.Message
{
    /// <summary>
    /// 信息命令
    /// </summary>

    [DataContract]
    public enum ECommandEnum
    {
        /// <summary>
        /// 退出
        /// </summary>
        [EnumMember]
        Exit = 0

    }
}
