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
        public static void Fatal(Type type, object message, Exception exception = null)
        {
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
        public static void Error(Type type, object message, Exception exception = null)
        {
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
        public static void Warn(Type type, object message, Exception exception = null)
        {
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
        public static void Info(Type type, object message, Exception exception = null)
        {
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
        public static void Debug(Type type, object message, Exception exception = null)
        {
            Init();
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
                log.Debug(message);
            else
                log.Debug(message, exception);
        }
    }
}