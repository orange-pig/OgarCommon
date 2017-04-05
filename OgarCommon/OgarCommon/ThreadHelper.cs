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
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(target), eventData);
            }
            catch (ApplicationException ex)
            {
                LogManager.Error("FaceWrap添加线程时应用程序错误," + ex.Message + "," + ex.ToString());
            }
            catch (OutOfMemoryException ex)
            {
                LogManager.Error("FaceWrap添加线程时内存不足," + ex.Message + "," + ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                LogManager.Error("FaceWrap添加线程时参数为空," + ex.Message + "," + ex.ToString());
            }
            catch (Exception ex)
            {
                LogManager.Error("FaceWrap添加线程时异常," + ex.Message + "," + ex.ToString());
            }
        }
    }
}
