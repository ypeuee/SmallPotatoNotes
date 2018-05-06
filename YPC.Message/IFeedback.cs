using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using YpeuEe.PublicModel.Message;

namespace YpeuEe.PublicContract.Message
{
    /// <summary>
    /// 反馈
    /// </summary>
    [ServiceContract(Name = "Common.Contract.Message.IFeedback", Namespace = "http://www.ypeuee.com", SessionMode = SessionMode.Required)]

    public interface IFeedback
    {
        /// <summary>
        /// 提交反馈
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool Submit(MFeedback model);
    }
}
