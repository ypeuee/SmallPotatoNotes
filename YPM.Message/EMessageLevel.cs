using System.Runtime.Serialization;

namespace YpeuEe.PublicModel.Message
{
    /// <summary>
    /// 信息级别
    /// </summary>

    [DataContract]
    public enum EMessageLevel
    {

        /// <summary>
        /// 不显示图标。
        /// </summary>
        [EnumMember]
        None = 0,
        /// <summary>
        /// 消息框显示一个“错误”图标。
        /// </summary>
        [EnumMember]
        Error = 16,
        /// <summary>
        /// 消息框显示一个“手形”图标。
        /// </summary>
        [EnumMember]
        Hand = 16,
        /// <summary>
        /// 消息框显示一个“停止”图标。
        /// </summary>
        [EnumMember]
        Stop = 16,
        /// <summary>
        /// 消息框显示一个“问号”图标。
        /// </summary>
        [EnumMember]
        Question = 32,
        /// <summary>
        /// 消息框显示一个“感叹号”图标。
        /// </summary>
        [EnumMember]
        Exclamation = 48,
        /// <summary>
        /// 消息框显示一个“警告”图标。
        /// </summary>
        [EnumMember]
        Warning = 48,
        /// <summary>
        /// 消息框显示一个“信息”图标。
        /// </summary>
        [EnumMember]
        Information = 64,
        /// <summary>
        ///  消息框显示一个“星号”图标。
        /// </summary>
        [EnumMember]
        Asterisk = 64,

    }
}
