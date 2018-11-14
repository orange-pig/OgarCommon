using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OgarCommon
{
    /// <summary>
    /// 提供线程的一个管理方法类
    /// </summary>
    public static class ThreadHelper
    {
        /// <summary>
        /// 转发方法到线程池中线程
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="target"></param>
        public static void ForwardActionToThreadPool(object eventData, Action<object> target)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(target), eventData);
        }
    }
}
