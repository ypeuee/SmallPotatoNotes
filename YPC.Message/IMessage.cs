using System.ServiceModel;
using YpeuEe.PublicModel.Message;

namespace YpeuEe.PublicContract.Message
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract(Name = "YpeuEe.PublicContract.Message.IMessage", Namespace = "http://www.ypeuee.com", SessionMode = SessionMode.Required, CallbackContract = typeof(IMessageCallback))]
    public interface IMessage
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [OperationContract]
        MUserInfo Login(MUserInfoLogin loginModel);

        /// <summary>
        /// 注册游客并登录用户登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [OperationContract]
        MUserInfo Login2(MUserInfoRegistered loginModel);

        /// <summary>
        /// 如果用户已经存在则直接登录，如果不存在，先注册后登录 如果已经存在，则尝试登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [OperationContract]
        MUserInfo Login3(MUserInfoRegistered loginModel);

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string SendVerificationCode(MUserInfoRegistered loginModel);


        /// <summary>
        /// 用户发送信息通道
        /// </summary>
        /// <param name="user"></param>
        [OperationContract]
        void SetCallback(MUserInfo user);

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="user"></param>
        [OperationContract]
        void Exit(MUserInfo user);

        /// <summary>
        ///  获取在线用户数
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int UserCount(MUserInfo user);

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="sendUser"></param>
        /// <param name="receiceUser"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [OperationContract]
        MSendMessageResult SendMessage(MUserInfo sendUser, MUserInfo receiceUser, MMessage message);
    }
}
