using System.ServiceModel;
using YpeuEe.PublicModel.Message;

namespace YpeuEe.PublicContract.Message
{
    /// <summary>
    /// 管理类，提供登录，获取用户列表，发送在线状态，退出等方法。
    /// </summary>
    [ServiceContract(Name = "Common.Contract.Message.IUser", Namespace = "http://www.ypeuee.com", SessionMode = SessionMode.Required)]
    public interface IUser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool SendVerificationCode();


        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        [OperationContract]
        string Add(MUserInfoRegistered user);
  
    }
}
