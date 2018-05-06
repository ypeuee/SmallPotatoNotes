namespace YpeuEe.PublicModel.Message
{
    /// <summary>
    /// 发送信息的结果
    /// </summary>
    //[DataContract]
    public class MSendMessageResult
    {
        /// <summary>
        /// true 成功 false失败
        /// </summary>
        //[DataMember]
        public bool Result { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        //[DataMember]
        public string Message { get; set; }
    }
}
