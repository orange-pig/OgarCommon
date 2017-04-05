using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace OgarCommon.Device.IDCard
{
    /// <summary>
    /// 精伦身份证读卡器
    /// 还未完成
    /// </summary>
    public class IDCard_JL : IIDCard
    {
        const string externDll = "sdtapi.dll";  //身份证读卡器库

        [DllImport(externDll, CallingConvention = CallingConvention.StdCall)]
        public static extern int InitComm(int iPort);

        [DllImport(externDll, CallingConvention = CallingConvention.StdCall)]
        public static extern int CloseComm();

        [DllImport(externDll, CallingConvention = CallingConvention.StdCall)]
        public static extern int Authenticate();

        [DllImport(externDll, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadBaseInfos(StringBuilder Name, StringBuilder Gender, StringBuilder Folk, StringBuilder BirthDay,
            StringBuilder Code, StringBuilder Address, StringBuilder Agency, StringBuilder ExpireStart, StringBuilder ExpireEnd);

        // 判断卡是否在设备上，1在，0不在
        [DllImport(externDll, CallingConvention = CallingConvention.StdCall)]
        public static extern int CardOn();

        private string m_sName;
        private string m_sCode;
        private string m_sGender;
        private string m_sFolk;
        private string m_sAddr;
        private string m_sValid;
        private string m_sPolice;
        int m_iYear;
        int m_iMonth;
        int m_iDay;
        DateTime m_dateValidEnd;

        StringBuilder Name = new StringBuilder(31);
        StringBuilder Gender = new StringBuilder(3);
        StringBuilder Folk = new StringBuilder(10);
        StringBuilder BirthDay = new StringBuilder(9);
        StringBuilder Code = new StringBuilder(19);
        StringBuilder Address = new StringBuilder(71);
        StringBuilder Agency = new StringBuilder(31);
        StringBuilder ExpireStart = new StringBuilder(9);
        StringBuilder ExpireEnd = new StringBuilder(9);

        /// <summary>
        /// 打开身份读卡器
        /// </summary>
        /// <param name="iPort">读卡器端口号</param>
        /// <returns></returns>
        public int Open(int iPort)
        {
            return IDCard_JL.InitComm(iPort);
        }
        /// <summary>
        /// 关闭身份证读卡器
        /// </summary>
        /// <returns></returns>
        public int Close()
        {
            return IDCard_JL.CloseComm();
        }
        /// <summary>
        /// 读取身份证信息
        /// </summary>
        /// <returns></returns>

        public int Read()
        {
            int iRet = 0;
            try
            {
                if (1 != IDCard_JL.Authenticate())
                {
                    return 0;
                }

                iRet = IDCard_JL.ReadBaseInfos(Name, Gender, Folk, BirthDay, Code, Address, Agency, ExpireStart, ExpireEnd);
                if (1 == iRet)
                {
                    m_sName = Name.ToString().Trim();
                    m_sCode = Code.ToString().Trim();
                    m_sGender = Gender.ToString().Trim();
                    m_sFolk = Folk.ToString().Trim();
                    m_sAddr = Address.ToString().Trim();
                    m_sPolice = Agency.ToString().Trim();

                    string sEndValid = ExpireEnd.ToString().Trim();
                    m_sValid = ExpireStart.ToString().Trim() + "-" + sEndValid;

                    if ("长期" == sEndValid)
                    {
                        m_dateValidEnd = DateTime.Now.AddYears(10);
                    }
                    else
                    {
                        m_dateValidEnd = DateTime.ParseExact(sEndValid, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    }

                    string sBirth = BirthDay.ToString();
                    m_iYear = Convert.ToInt32(sBirth.Substring(0, 4));
                    m_iMonth = Convert.ToInt32(sBirth.Substring(4, 2));
                    m_iDay = Convert.ToInt32(sBirth.Substring(6, 2));
                }
            }
            catch (Exception exec)
            {
                return -1;
            }

            return iRet;
        }

        public IDCardInfo ReadIDCardInfo()
        {
            IDCardInfo info = new IDCardInfo();
            info.Name = m_sName;
            info.GenderCode = m_sCode;
            info.Authority = m_sPolice;
            info.GenderCName = m_sGender;
            info.NationCName = m_sFolk;
            info.Address = m_sAddr;
            if (m_iYear != 0 && m_iMonth != 0 && m_iDay != 0)
            {
                info.BirthDay = new DateTime(m_iYear, m_iMonth, m_iDay);
            }
            //info.ExpireStartData
            info.ExpireEndData = m_dateValidEnd;
            return info;
        }

    }
}
