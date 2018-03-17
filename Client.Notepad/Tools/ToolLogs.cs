using log4net;
using System;
using System.IO;

namespace Client.Notepad.Tools
{
    /// <summary>
    /// 日志记录工具
    /// </summary>
    public class ToolLogs
    {
        private static bool _isInit;
        public static void Init()
        {
            if (!_isInit)
            {
                _isInit = true;
                log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
            }
        }

        /// <summary>
        /// Log a message object with the log4net.Core.Level.Fatal level.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Fatal(Type type=null, object message = null, Exception exception = null)
        {
            if (type == null)
                type = GetClassType();

            if (message == null)
                message = GetMethodName();

            Init();
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
                log.Fatal(message);
            else
                log.Fatal(message, exception);
        }
 

        /// <summary>
        /// Logs a message object with the log4net.Core.Level.Error level.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Error(Type type=null, object message=null, Exception exception = null)
        {
            if (type == null)
                type = GetClassType();

            if (message == null)
                message = GetMethodName();

            Init();
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
                log.Error(message);
            else
                log.Error(message, exception);
        }

        /// <summary>
        /// Log a message object with the log4net.Core.Level.Warn level.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Warn(Type type = null, object message = null, Exception exception = null)
        {
            if (type == null)
                type = GetClassType();

            if (message == null)
                message = GetMethodName();

            Init();
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
                log.Warn(message);
            else
                log.Warn(message, exception);
        }

        /// <summary>
        ///  Logs a message object with the log4net.Core.Level.Info level.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Info(Type type = null, object message = null, Exception exception = null)
        {
            if (type == null)
                type = GetClassType();

            if (message == null)
                message = GetMethodName();

            Init();
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
                log.Info(message);
            else
                log.Info(message, exception);
        }

        /// <summary>
        /// Log a message object with the log4net.Core.Level.Debug level.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Debug(Type type = null, object message = null, Exception exception = null)
        {
            if (type == null)
                type = GetClassType();

            if (message == null)
                message = GetMethodName();

            Init();
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
                log.Debug(message);
            else
                log.Debug(message, exception);
        }


        /// <summary>
        /// 获取类名与方法名
        /// </summary>
        /// <returns></returns>
        public static string GetClassMethodName()
        {
            //StackFrame frame = new StackFrame(1);       //偏移一个函数位,也即是获取当前函数的前一个调用函数
            //MethodBase method = frame.GetMethod();      //取得调用函数
            //Console.WriteLine(method.Name);  //a1

            try
            {

                System.Reflection.MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
                string className = "方法:" + method.Name;
                if (method.ReflectedType != null)
                {
                    className = string.Format("类名:{0}{1}", method.ReflectedType.Name, className);
                }

                return className;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取类名
        /// </summary>
        /// <returns></returns>
        public static string GetClassName()
        {
            return GetClassType().Name;
        }

        /// <summary>
        /// 获取类名
        /// </summary>
        /// <returns></returns>
        public static Type GetClassType()
        {
            try
            {
                System.Reflection.MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
                if (method.ReflectedType != null)
                {
                    return method.ReflectedType;
                }
                return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            }
            catch (Exception)
            {
                return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            }
        }

        /// <summary>
        /// 获取方法名
        /// </summary>
        /// <returns></returns>
        public static string GetMethodName()
        {
            try
            {
                System.Reflection.MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
                return method.Name;
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
    }
}