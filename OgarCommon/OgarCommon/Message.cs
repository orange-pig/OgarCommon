using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgarCommon
{
    /// <summary>
    /// 发送消息字段与状态的类
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 需要发送的消息内容
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// 请求状态枚举
        /// </summary>
        public Status Status { get; private set; }

        /// <summary>
        /// Message构造函数
        /// </summary>
        /// <param name="text">需要发送的消息内容</param>
        /// <param name="s">状态枚举</param>
        public Message(string text, Status status)
        {
            this.Text = text;
            this.Status = status;
        }
    }

    /// <summary>
    /// 请求状态枚举类
    /// </summary>
    public enum Status
    {
        Error = 0,
        Alert = 1,

        Ok = 200,
        NoContentOk = 204,

        BadRequest = 400,
        Forbidden = 402,
        NotFound = 404,
        MethodNotAllowed = 405,
        OprationFailed = 406,

        ServiceUnavailable = 503,
    }
}
