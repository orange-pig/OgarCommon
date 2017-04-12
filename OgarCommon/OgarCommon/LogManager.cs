using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace OgarCommon
{
    // 日志管理类，日志的操作
    public static class LogManager
    {
        private static Logger logger = NLog.LogManager.GetLogger("default");

        /// <summary>
        /// 添加错误日志
        /// </summary>
        /// <param name="obj"></param>
        public static void Error(object obj)
        {
            logger.Error(obj);
        }

        public static void Error(string message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// 添加异常错误日志
        /// </summary>
        /// <param name="obj"></param>
        public static void Error(Exception ex)
        {
            logger.Error(ex.Message + "\r\n" 
                + ex.ToString() + "\r\n" 
                + ex.StackTrace);
        }

        public static void Error(string msg, Exception ex)
        {
            logger.Error(msg + "\r\n" 
                + ex.Message + "\r\n" 
                + ex.ToString() + "\r\n"
                + ex.StackTrace);
        }

        /// <summary>
        /// 添加日志记录
        /// </summary>
        /// <param name="obj"></param>
        public static void Info(object obj)
        {
            logger.Info(obj);
        }

        public static void Info(string message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// 添加调试日志
        /// </summary>
        /// <param name="obj"></param>
        public static void Debug(object obj)
        {
            logger.Debug(obj);
        }

        public static void Debug(string message)
        {
            logger.Debug(message);
        }

        /// <summary>
        /// 添加异常调试日志
        /// </summary>
        /// <param name="obj"></param>
        public static void Debug(Exception ex)
        {
            logger.Debug(ex.Message + "\r\n" 
                + ex.ToString() + "\r\n"
                + ex.StackTrace);
        }

        /// <summary>
        /// 添加致命错误信息
        /// </summary>
        /// <param name="obj"></param>
        public static void Fatal(object obj)
        {
            logger.Fatal(obj);
        }

        public static void Fatal(string message)
        {
            logger.Fatal(message);
        }

        /// <summary>
        /// 添加异常致命错误日志
        /// </summary>
        /// <param name="obj"></param>
        public static void Fatal(Exception ex)
        {
            logger.Fatal(ex.Message + "\r\n" 
                + ex.ToString() + "\r\n"
                + ex.StackTrace);
        }

        /// <summary>
        /// 添加警告日志
        /// </summary>
        /// <param name="obj"></param>
        public static void Warn(object obj)
        {
            logger.Warn(obj);
        }

        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        /// <summary>
        /// 添加异常警告日志
        /// </summary>
        /// <param name="obj"></param>
        public static void Warn(Exception ex)
        {
            logger.Warn(ex.Message + "\r\n" 
                + ex.ToString() + "\r\n"
                + ex.StackTrace);
        }

        /// <summary>
        /// 添加轻量影响日志
        /// </summary>
        /// <param name="obj"></param>
        public static void Trace(object obj)
        {
            logger.Trace(obj);
        }

        public static void Trace(string message)
        {
            logger.Trace(message);
        }

        /// <summary>
        /// 添加异常轻量级日志
        /// </summary>
        /// <param name="obj"></param>
        public static void Trace(Exception ex)
        {
            logger.Trace(ex.Message + "\r\n" 
                + ex.ToString() + "\r\n"
                + ex.StackTrace);
        }
    }
}
