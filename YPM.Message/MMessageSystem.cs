using System.Runtime.Serialization;

namespace YpeuEe.PublicModel.Message
{
    /// <summary>
    /// 系统信息
    /// </summary>
    [DataContract]
    public class MMessageSystem : MMessage
    {
        /// <summary>
        /// 信息命令
        /// </summary>
        public ECommandEnum CommandEnum { get; set; }

    }
}
