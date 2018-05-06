using System.ServiceModel;
using YpeuEe.PublicModel.Message;

namespace YpeuEe.PublicContract.Message
{
    /// <summary>
    /// 信息回拨
    /// </summary>
    [ServiceContract(Name = "YpeuEe.PublicContract.Message.IMessageCallback", Namespace = "http://www.ypeuee.com", SessionMode = SessionMode.Required)]
    public interface IMessageCallback
    {
        /// <summary>
        /// 接收用户通过服务器转发送的信息
        /// </summary>
        /// <param name="message">信息内容</param>
        [OperationContract(IsOneWay = true)]
        void ReceiveMessageSystem(MMessageSystem message);

        /// <summary>
        /// 接收用户通过服务器转发送的信息
        /// </summary>
        /// <param name="sendUserInfo">发送人信息</param>
        /// <param name="message">信息内容</param>
        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(MUserInfo sendUserInfo, MMessage message );

        /// <summary>
        /// 接收用户通过服务器转发送的信息
        /// </summary>
        /// <param name="sendUserInfo">发送人信息</param>
        /// <param name="receiveUserInfo">接收人信息</param>
        /// <param name="message">信息内容</param>
        [OperationContract(IsOneWay = true)]
        void ReceiveMessageInfo(MUserInfo sendUserInfo, MUserInfo receiveUserInfo, MMessage message);

        /// <summary>
        /// 接收用户在其它地方登录时通过服务器转发送的下线信息
        /// </summary>
        /// <param name="message">信息内容</param>
        [OperationContract(IsOneWay = true)]
        void ExecExit(MMessageSystem message);


    }
}
