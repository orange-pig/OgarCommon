using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;

namespace OgarCommon.Device.IDCard
{
    /// <summary>
    /// 通用身份读卡器
    /// </summary>
    public class IDCard_YA : IIDCard
    {
        const string externDll = @"Sdtapi.dll";  //身份证读卡器库
        //首先，声明通用接口
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_OpenPort(int iPortID);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_ClosePort(int iPortID);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_PowerManagerBegin(int iPortID, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_AddSAMUser(int iPortID, string pcUserName, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_SAMLogin(int iPortID, string pcUserName, string pcPasswd, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_SAMLogout(int iPortID, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_UserManagerOK(int iPortID, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_ChangeOwnPwd(int iPortID, string pcOldPasswd, string pcNewPasswd, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_ChangeOtherPwd(int iPortID, string pcUserName, string pcNewPasswd, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_DeleteSAMUser(int iPortID, string pcUserName, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_StartFindIDCard(int iPortID, ref int pucIIN, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_SelectIDCard(int iPortID, ref int pucSN, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_ReadBaseMsg(int iPortID, string pucCHMsg, ref int puiCHMsgLen, string pucPHMsg, ref int puiPHMsgLen, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_ReadBaseMsgToFile(int iPortID, string fileName1, ref int puiCHMsgLen, string fileName2, ref int puiPHMsgLen, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_WriteAppMsg(int iPortID, ref byte pucSendData, int uiSendLen, ref byte pucRecvData, ref int puiRecvLen, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_WriteAppMsgOK(int iPortID, ref byte pucData, int uiLen, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_CancelWriteAppMsg(int iPortID, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_ReadNewAppMsg(int iPortID, ref byte pucAppMsg, ref int puiAppMsgLen, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_ReadAllAppMsg(int iPortID, ref byte pucAppMsg, ref int puiAppMsgLen, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_UsableAppMsg(int iPortID, ref byte ucByte, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_GetUnlockMsg(int iPortID, ref byte strMsg, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_GetSAMID(int iPortID, ref byte StrSAMID, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_SetMaxRFByte(int iPortID, byte ucByte, int iIfOpen);
        [DllImport(externDll, CallingConvention = CallingConvention.Winapi)]
        public static extern int SDT_ResetSAM(int iPortID, int iIfOpen);
        [DllImport("WltRS.dll")]
        public static extern int GetBmp(string file_name, int intf);

 //       public delegate void De_ReadICCardComplete(clsEDZ objEDZ);
//        public event De_ReadICCardComplete ReadICCardComplete;
        private IDCardInfo objEDZ = new IDCardInfo();
        private int EdziIfOpen = 0; //自动开关串口
        int EdziPortID = 1;
        public IDCard_YA() { }
        public bool ReadICCard(int iPort)
        {
            //     bool bUsbPort = false;
            int intOpenPortRtn = 0;

            objEDZ = new IDCardInfo(); //检测 usb 口的机具连接，必须先检测 usb
            for (int i = iPort; i <= 1016; i++)
            {
                intOpenPortRtn = SDT_OpenPort(i);
                if (intOpenPortRtn == 144)
                {
                    EdziPortID = i;
                    //        bUsbPort = true;
                    return true;
                }
            }
            return false;
        }

        private const string idCardPhoto = "zp.bmp";

        //检测串口的机具连接
        //EdziPortID = iPort;
        //bUsbPort = true;


        //if (!bUsbPort)
        //{
        //    for (iPort = 1; iPort <= 2; iPort++)
        //    {
        //        intOpenPortRtn = SDT_OpenPort(iPort);
        //        if (intOpenPortRtn == 144)
        //        {
        //            EdziPortID = iPort;
        //            bUsbPort = false;
        //            break;
        //        }
        //    }
        //}
        ////下面找卡
        //rtnTemp = SDT_StartFindIDCard(EdziPortID, ref pucIIN, EdziIfOpen);
        //if (rtnTemp != 159)
        //{
        //    rtnTemp = SDT_StartFindIDCard(EdziPortID, ref pucIIN, EdziIfOpen); //再找卡
        //    if (rtnTemp != 159) { 
        //        rtnTemp = SDT_ClosePort(EdziPortID);
        //        //MessageBox.Show(" 未 放 卡 或 者 卡 未 放 好 ， 请 重 新 放 卡 ！", " 提 示", MessageBoxButtons.OK, MessageBoxIcon.Error); 
        //        return false; 
        //    }
        //} 
        ////选卡
        //rtnTemp = SDT_SelectIDCard(EdziPortID, ref pucSN, EdziIfOpen); 
        //if (rtnTemp != 144)
        //{
        //    rtnTemp = SDT_SelectIDCard(EdziPortID, ref pucSN, EdziIfOpen); //再选卡
        //    if (rtnTemp != 144)
        //    {
        //        rtnTemp = SDT_ClosePort(EdziPortID); MessageBox.Show("读卡失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //} 
        ////注意，在这里，用户必须有应用程序当前目录的读写权限//检测串口的机具连接
        //if (!bUsbPort)
        //{
        //    for (iPort = 1; iPort <= 2; iPort++)
        //    {
        //        intOpenPortRtn = SDT_OpenPort(iPort);
        //        if (intOpenPortRtn == 144)
        //        {
        //            EdziPortID = iPort;
        //            bUsbPort = false;
        //            break;
        //        }
        //    }
        //}
        //if (intOpenPortRtn != 144)
        //{
        //    MessageBox.Show(" 端 口 打 开 失 败 ， 请 检 测 相 应 的 端 口 或 者 重 新 连 接 读 卡 器 ！ ", " 提 示 ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    return false;
        //} 


        public int Open(int iPort)
        {
            if (ReadICCard(iPort))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        public int Read()
        {
            //在这里，如果您想下一次不用再耗费检查端口的检查的过程，您可以把 EdziPortID 保存下来，可以 保存在注册表中，也可以保存在配置文件中，我就不多写了，但是，您要考虑机具连接端口被用户改变的情况哦 
            //下面找卡

            int pucIIN = 0;
            int pucSN = 0;
            int puiCHMsgLen = 0;
            int puiPHMsgLen = 0;
            int rtnTemp = SDT_StartFindIDCard(EdziPortID, ref pucIIN, EdziIfOpen);
            if (rtnTemp != 159)
            {
                rtnTemp = SDT_StartFindIDCard(EdziPortID, ref pucIIN, EdziIfOpen); //再找卡
                if (rtnTemp != 159)
                {
                    //MessageBox.Show(" 未 放 卡 或 者 卡 未 放 好 ， 请 重 新 放 卡 ！ ", " 提 示 ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //       rtnTemp = SDT_ClosePort(EdziPortID);
                    return 0;
                }
            }
                
            //选卡
            rtnTemp = SDT_SelectIDCard(EdziPortID, ref pucSN, EdziIfOpen);
            if (rtnTemp != 144)
            {
                rtnTemp = SDT_SelectIDCard(EdziPortID, ref pucSN, EdziIfOpen); //再选卡
                if (rtnTemp != 144)
                {
                    //MessageBox.Show("读卡失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //      rtnTemp = SDT_ClosePort(EdziPortID);
                    return 0;
                }
            }
            //注意，在这里，用户必须有应用程序当前目录的读写权限 
            FileInfo objFile = new FileInfo("wz.txt");
            if (objFile.Exists)
            {
                objFile.Attributes = FileAttributes.Normal;
                objFile.Delete();
            }
            objFile = new FileInfo("zp.bmp");
            if (objFile.Exists)
            {
                objFile.Attributes = FileAttributes.Normal;
                objFile.Delete();
            }
            objFile = new FileInfo("zp.wlt");
            if (objFile.Exists)
            {
                objFile.Attributes = FileAttributes.Normal;
                objFile.Delete();
            }
            rtnTemp = SDT_ReadBaseMsgToFile(EdziPortID, "wz.txt", ref puiCHMsgLen, "zp.wlt", ref puiPHMsgLen, EdziIfOpen);
            if (rtnTemp != 144)
            {
                //rtnTemp = SDT_ClosePort(EdziPortID); MessageBox.Show("读卡失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            //下面解析照片，注意，如果在 C 盘根目录下没有机具厂商的授权文件 Termb.Lic，照片解析将会失败
            if (EdziPortID < 17)  
            {
                rtnTemp = GetBmp("zp.wlt", 1);    // 串口
            } 
            else
            {
                rtnTemp = GetBmp("zp.wlt", 2);    // USB
            }
            
            /*     if (bUsbPort)
                     rtnTemp = GetBmp("zp.wlt", 2);
                 else
                     rtnTemp = GetBmp("zp.wlt", 1);*/
            if (1 != rtnTemp)
            {
                return -1;
            }


    /*        switch (rtnTemp)
            {
                case 0: MessageBox.Show("调用 sdtapi.dll 错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 1: //正常
                    break;
                case -1: MessageBox.Show("相片解码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -2: MessageBox.Show("wlt 文件后缀错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -3: MessageBox.Show("wlt 文件打开错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -4: MessageBox.Show("wlt 文件格式错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -5: MessageBox.Show("软件未授权！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -6:
                    MessageBox.Show("设备连接错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }*/


            //      rtnTemp = SDT_ClosePort(EdziPortID);
            FileInfo f = new FileInfo("wz.txt");
            FileStream fs = f.OpenRead();
            byte[] bt = new byte[fs.Length];
            fs.Read(bt, 0, (int)fs.Length);
            fs.Close();
            string str = System.Text.UnicodeEncoding.Unicode.GetString(bt);
            objEDZ.Name = System.Text.UnicodeEncoding.Unicode.GetString(bt, 0, 30).Trim();
            objEDZ.GenderCode = System.Text.UnicodeEncoding.Unicode.GetString(bt, 30, 2).Trim();
            objEDZ.NationCode = System.Text.UnicodeEncoding.Unicode.GetString(bt, 32, 4).Trim();
            string strBird = System.Text.UnicodeEncoding.Unicode.GetString(bt, 36, 16).Trim();
            objEDZ.BirthDay = Convert.ToDateTime(strBird.Substring(0, 4) + "年" + strBird.Substring(4, 2) + "月" + strBird.Substring(6) + "日");
            objEDZ.Address = System.Text.UnicodeEncoding.Unicode.GetString(bt, 52, 70).Trim();
            objEDZ.IDCardNumber = System.Text.UnicodeEncoding.Unicode.GetString(bt, 122, 36).Trim();
            objEDZ.Authority = System.Text.UnicodeEncoding.Unicode.GetString(bt, 158, 30).Trim();
            string strTem = System.Text.UnicodeEncoding.Unicode.GetString(bt, 188, bt.GetLength(0) - 188).Trim();
            objEDZ.ExpireStartData = Convert.ToDateTime(strTem.Substring(0, 4) + "年" + strTem.Substring(4, 2) + "月" + strTem.Substring(6, 2) + "日");
            strTem = strTem.Substring(8);
            if (strTem.Trim() != "长期")
            {
                objEDZ.ExpireEndData = Convert.ToDateTime(strTem.Substring(0, 4) + "年" + strTem.Substring(4, 2) + "月 " + strTem.Substring(6, 2) + "日");
            }
            else
            {
                objEDZ.ExpireEndData = DateTime.MaxValue;
            }
            if (File.Exists("zp.bmp"))
            {
                File.Copy("zp.bmp", "photo.bmp", true);

                //using (Image img = Image.FromFile("zp.bmp"))
                //{
                //    //objEDZ.PicImage = (Image)img.Clone();
                //    System.IO.MemoryStream m = new MemoryStream();
                //    img.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
                //    objEDZ.PicByte = m.ToArray();
                //    img.Dispose();
                //}
            }
            //ReadICCardComplete(objEDZ);
            return 1;
        }

        public IDCardInfo ReadIDCardInfo()
        {
            return objEDZ;
        }

        public int Close()
        {
            SDT_ClosePort(EdziPortID);
            return 1;
        }
    }
}

