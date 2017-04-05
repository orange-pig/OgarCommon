using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OgarCommon
{
    public class ExceptionThrower
    {
        private bool isRunnig;
        private Thread throwThread;

        private ConcurrentQueue<Exception> exceptionQueue;

        private static object locker = new object();
        private static ExceptionThrower instance;
        public static ExceptionThrower Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new ExceptionThrower();
                        }
                    }
                }
                return instance;
            }
        }


        public event EventHandler<Exception> ThorwedException;


        private ExceptionThrower()
        {
            exceptionQueue = new ConcurrentQueue<Exception>();

            throwThread = new Thread(DoThrow);
            throwThread.Name = "抛出异常线程";
        }


        public void Start()
        {
            isRunnig = true;
            throwThread.Start();
        }

        public void Stop()
        {
            isRunnig = false;
            while (true)
            {
                Exception exception;
                if (!exceptionQueue.TryDequeue(out exception))
                {
                    break;
                }
            }
        }

        public void ThrowException(Exception exp)
        {
            exceptionQueue.Enqueue(exp);
        }

        public void ThrowException(string msg)
        {
            exceptionQueue.Enqueue(new Exception(msg));
        }

        private void DoThrow()
        {
            while (isRunnig)
            {
                if (!SpinWait.SpinUntil(() => exceptionQueue.Count > 0, 500))
                {
                    continue;
                }

                Exception exception;
                if (!exceptionQueue.TryDequeue(out exception))
                {
                    continue;
                }

                if (exception == null)
                {
                    continue;
                }

                ThorwedException?.BeginInvoke(this, exception, null, null);
            }
        }
    }
}
