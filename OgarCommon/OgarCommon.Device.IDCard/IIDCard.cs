using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgarCommon.Device.IDCard
{
    public interface IIDCard
    {        /// <summary>
        /// 打开身份读卡器
        /// </summary>
        /// <param name="iPort">读卡器端口号</param>
        /// <returns></returns>
        int Open(int iPort);
        /// <summary>
        /// 关闭身份证读卡器
        /// </summary>
        /// <returns></returns>
        int Close();
        /// <summary>
        /// 读取身份证信息
        /// </summary>
        /// <param name="Active">区段</param>
        /// <returns></returns>
        int Read();

        IDCardInfo ReadIDCardInfo();
    }
}
