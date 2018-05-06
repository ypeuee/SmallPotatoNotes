using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Client.Notepad.Tools;
using Client.Pub.ServiceProxyFactoryLib;
using Commons.Tools.Computer;
using YpeuEe.PublicContract.Message;

namespace Client.Notepad
{
    class Class2
    {
        public static void Logig()
        {
            YpeuEe.PublicModel.Message.MUserInfoRegistered user = new YpeuEe.PublicModel.Message.MUserInfoRegistered();
            user.ComputerId = ComputerManage.Computer.ComputerId;
            //user.Code = TxtCode.Text.Trim();
            //user.Password = TxtPassword.Text;

            IMessage iMessage = SPF.Create<IMessage>("YpeuEe.ServerService.Message.SMessage", RCT.NoneMode, SMessageCallback.MessageCallback);

            try
            {
                SystemCommon.UserInfo = iMessage.Login2(user);

                //MessageBox.Show(SystemCommon.UserInfo.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }








    }

    class SMessageCallback : IMessageCallback
    {
        private static SMessageCallback _messageCallback;

        internal static SMessageCallback MessageCallback
        {
            get
            {
                if (_messageCallback == null)
                    _messageCallback = new SMessageCallback();

                return _messageCallback;
            }

        }

        private SMessageCallback()
        {

        }

        /// <summary>
        /// 接收用户在其它地方登录时通过服务器转发送的下线信息
        /// </summary>
        /// <param name="message"></param>
        public void ExecExit(YpeuEe.PublicModel.Message.MMessageSystem message)
        {
            //MessageBox.Show(message.Message);
            switch (message.CommandEnum)
            {
                case YpeuEe.PublicModel.Message.ECommandEnum.Exit:

                    break;
            }
        }

        /// <summary>
        /// 接收用户通过服务器转发送的信息
        /// </summary>
        /// <param name="sendUserInfo"></param>
        /// <param name="message"></param>
        public void ReceiveMessage(YpeuEe.PublicModel.Message.MUserInfo sendUserInfo, YpeuEe.PublicModel.Message.MMessage message)
        {

        }


        /// <summary>
        /// 接收用户通过服务器转发送的信息
        /// </summary>
        /// <param name="sendUserInfo"></param>
        /// <param name="receiveUserInfo"></param>
        /// <param name="message"></param>
        public void ReceiveMessageInfo(YpeuEe.PublicModel.Message.MUserInfo sendUserInfo, YpeuEe.PublicModel.Message.MUserInfo receiveUserInfo, YpeuEe.PublicModel.Message.MMessage message)
        {

        }

        /// <summary>
        /// 接收用户通过服务器转发送的信息
        /// </summary>
        /// <param name="message"></param>
        public void ReceiveMessageSystem(YpeuEe.PublicModel.Message.MMessageSystem message)
        {
            throw new NotImplementedException();
        }
    }


    public class ComputerManage
    {
        private static ComputerM _computer;

        public static ComputerM Computer
        {
            get
            {
                if (_computer == null)
                {
                    Init();
                }
                return _computer;
            }

        }

        ///// <summary>
        ///// 初始化电脑信息
        ///// </summary>
        //public static void InitTask()
        //{
        //   Task.Run(() =>
        //    {
        //        try
        //        {
        //            Init();
        //        }
        //        catch (Exception ex)
        //        {
        //            //ToolLogs.Error(ex);

        //        }

        //    });
        //}

        /// <summary>
        /// 初始化电脑信息
        /// </summary>
        public static void Init()
        {
            _computer = new ComputerM();
            try
            {
                //MessageBox.Show(SystemInfo.GetSystemType());
                 
                _computer.ComputerName = SystemInfo.GetComputerName();
               
                _computer.Mac = SystemInfo.GetMacAddress();
                _computer.SystemType = SystemInfo.GetSystemType();
                _computer.UserName = SystemInfo.GetUserName();
                _computer.DiskId = SystemInfo.GetDiskID();
                _computer.CpuId = SystemInfo.GetCpuID();


                //_computer.Read_CA = SystemInfo.Read_CA();
            }
            catch (Exception e)
            {

            }

        }



    }

    public class ComputerM
    {
        /// <summary>
        /// mac地址
        /// </summary>
        public string Mac { get; set; }

        /// <summary>
        /// cpuID
        /// </summary>
        public string CpuId { get; set; }

        /// <summary>
        /// 硬盘ID
        /// </summary>
        public string DiskId { get; set; }

        /// <summary>
        /// 电脑名称
        /// </summary>
        public string ComputerName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 证书
        /// </summary>
        public string Read_CA { get; set; }

        /// <summary>
        /// 系统类型
        /// </summary>
        public string SystemType { get; set; }

        private string _computId;
        /// <summary>
        /// 电脑ID
        /// </summary>
        public string ComputerId
        {
            get
            {
                if (_computId == null)
                    _computId = MD5Encrypt(CpuId + DiskId + SystemType + ComputerName + UserName, new UTF8Encoding());
                return _computId;
            }
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public static string MD5Encrypt(string input, Encoding encode)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(encode.GetBytes(input));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            return sb.ToString();
        }
    }

}
