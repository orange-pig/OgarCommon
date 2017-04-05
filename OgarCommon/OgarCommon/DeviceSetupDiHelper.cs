using OgarCommon.Win32Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace OgarCommon
{
    ///<summary>
    /// 设备类型
    /// </summary>
    public class DeviceClasses
    {
        public static Guid ClassesGuid;

        /// <summary>
        /// 枚举设备类型
        /// </summary>
        /// <param name="ClassIndex"></param>
        /// <param name="ClassName">设备类型名称</param>
        /// <param name="ClassDescription">设备类型说明</param>
        /// <param name="DevicePresent"></param>
        /// <returns></returns>
        public static int EnumerateClasses(UInt32 ClassIndex, StringBuilder ClassName, StringBuilder ClassDescription, ref bool DevicePresent)
        {
            Guid ClassGuid = Guid.Empty;
            IntPtr NewDeviceInfoSet;
            UInt32 result;
            SP_DEVINFO_DATA DeviceInfoData = new SP_DEVINFO_DATA();
            bool resNam = false;
            UInt32 RequiredSize = 0;
            result = CfgMgr32.CM_Enumerate_Classes(ClassIndex, ref ClassGuid, 0);
            DevicePresent = false;
            SP_CLASSIMAGELIST_DATA imagelist = new SP_CLASSIMAGELIST_DATA();
            if (result != CfgMgr32.CR_SUCCESS)
            {
                return (int)result;
            }
            resNam = SetupApi.SetupDiClassNameFromGuidA(ref ClassGuid, ClassName, RequiredSize, ref RequiredSize);
            if (RequiredSize > 0)
            {
                ClassName.Capacity = (int)RequiredSize;
                resNam = SetupApi.SetupDiClassNameFromGuidA(ref ClassGuid, ClassName, RequiredSize, ref RequiredSize);
            }
            NewDeviceInfoSet = SetupApi.SetupDiGetClassDevsA(ref ClassGuid, 0, IntPtr.Zero, SetupApi.DIGCF_PRESENT);
            if (NewDeviceInfoSet.ToInt32() == -1)
            {
                DevicePresent = false;
                return 0;
            }

            UInt32 numD = 0;
            DeviceInfoData.cbSize = 28;
            DeviceInfoData.DevInst = 0;
            DeviceInfoData.ClassGuid = System.Guid.Empty;
            DeviceInfoData.Reserved = 0;

            Boolean res1 = SetupApi.SetupDiEnumDeviceInfo(
            NewDeviceInfoSet,
            numD,
            DeviceInfoData);

            if (!res1)
            {
                DevicePresent = false;
                return 0;
            }
            SetupApi.SetupDiDestroyDeviceInfoList(NewDeviceInfoSet);
            IntPtr KeyClass = SetupApi.SetupDiOpenClassRegKeyExA(
                ref ClassGuid, SetupApi.MAXIMUM_ALLOWED, SetupApi.DIOCR_INSTALLER, IntPtr.Zero, 0);
            if (KeyClass.ToInt32() == -1)
            {
                DevicePresent = false;
                return 0;
            }
            UInt32 sizeB = AdvApi32.MAX_SIZE_DEVICE_DESCRIPTION;
            ClassDescription.Capacity = AdvApi32.MAX_SIZE_DEVICE_DESCRIPTION;
            UInt32 res = AdvApi32.RegQueryValueA(KeyClass, 0, ClassDescription, ref sizeB);
            if (res != 0) ClassDescription = new StringBuilder(""); //No device description
            DevicePresent = true;
            ClassesGuid = DeviceInfoData.ClassGuid;
            return 0;
        }
    }

    ///<summary>
    /// 设备详细
    /// </summary>
    public class DeviceInfo
    {
       
        /// <summary>
        /// 通过设备类型枚举设备信息
        /// </summary>
        /// <param name="DeviceIndex"></param>
        /// <param name="ClassName"></param>
        /// <param name="DeviceName"></param>
        /// <returns></returns>
        public static int EnumerateDevices(UInt32 DeviceIndex, string ClassName, StringBuilder DeviceName, StringBuilder DeviceID, StringBuilder Mfg, StringBuilder IsInstallDrivers)
        {
            UInt32 RequiredSize = 0;
            Guid guid = Guid.Empty;
            Guid[] guids = new Guid[1];
            IntPtr NewDeviceInfoSet;
            SP_DEVINFO_DATA DeviceInfoData = new SP_DEVINFO_DATA();

            bool res = SetupApi.SetupDiClassGuidsFromNameA(ClassName, ref guids[0], RequiredSize, ref RequiredSize);
            if (RequiredSize == 0)
            {
                //类型不正确
                DeviceName = new StringBuilder("");
                return -2;
            }

            if (!res)
            {
                guids = new Guid[RequiredSize];
                res = SetupApi.SetupDiClassGuidsFromNameA(ClassName, ref guids[0], RequiredSize, ref RequiredSize);

                if (!res || RequiredSize == 0)
                {
                    //类型不正确
                    DeviceName = new StringBuilder("");
                    return -2;
                }
            }

            //通过类型获取设备信息
            NewDeviceInfoSet = SetupApi.SetupDiGetClassDevsA(ref guids[0], 0, IntPtr.Zero, SetupApi.DIGCF_PRESENT);
            if (NewDeviceInfoSet.ToInt32() == -1)
            {
                //设备不可用
                DeviceName = new StringBuilder("");
                return -3;
            }

            DeviceInfoData.cbSize = 28;
            //正常状态
            DeviceInfoData.DevInst = 0;
            DeviceInfoData.ClassGuid = System.Guid.Empty;
            DeviceInfoData.Reserved = 0;

            res = SetupApi.SetupDiEnumDeviceInfo(NewDeviceInfoSet,
                   DeviceIndex, DeviceInfoData);
            if (!res)
            {
                //没有设备
                SetupApi.SetupDiDestroyDeviceInfoList(NewDeviceInfoSet);
                DeviceName = new StringBuilder("");
                return -1;
            }

            DeviceName.Capacity = SetupApi.MAX_DEV_LEN;
            DeviceID.Capacity = SetupApi.MAX_DEV_LEN;
            Mfg.Capacity = SetupApi.MAX_DEV_LEN;
            IsInstallDrivers.Capacity = SetupApi.MAX_DEV_LEN;
            if (!SetupApi.SetupDiGetDeviceRegistryPropertyA(NewDeviceInfoSet, DeviceInfoData,
            SetupApi.SPDRP_FRIENDLYNAME, 0, DeviceName, SetupApi.MAX_DEV_LEN, IntPtr.Zero))
            {
                res = SetupApi.SetupDiGetDeviceRegistryPropertyA(NewDeviceInfoSet,
                 DeviceInfoData, SetupApi.SPDRP_DEVICEDESC, 0, DeviceName, SetupApi.MAX_DEV_LEN, IntPtr.Zero);
                if (!res)
                {
                    //类型不正确
                    SetupApi.SetupDiDestroyDeviceInfoList(NewDeviceInfoSet);
                    DeviceName = new StringBuilder("");
                    return -4;
                }
            }
            //设备ID
            bool resHardwareID = SetupApi.SetupDiGetDeviceRegistryPropertyA(NewDeviceInfoSet,
             DeviceInfoData, SetupApi.SPDRP_HARDWAREID, 0, DeviceID, SetupApi.MAX_DEV_LEN, IntPtr.Zero);
            if (!resHardwareID)
            {
                //设备ID未知
                DeviceID = new StringBuilder("");
                DeviceID.Append("未知");
            }
            //设备供应商
            bool resMfg = SetupApi.SetupDiGetDeviceRegistryPropertyA(NewDeviceInfoSet,
             DeviceInfoData, SetupApi.SPDRP_MFG, 0, Mfg, SetupApi.MAX_DEV_LEN, IntPtr.Zero);
            if (!resMfg)
            {
                //设备供应商未知
                Mfg = new StringBuilder("");
                Mfg.Append("未知");
            }
            //设备是否安装驱动
            bool resIsInstallDrivers = SetupApi.SetupDiGetDeviceRegistryPropertyA(NewDeviceInfoSet,
             DeviceInfoData, SetupApi.SPDRP_DRIVER, 0, IsInstallDrivers, SetupApi.MAX_DEV_LEN, IntPtr.Zero);
            if (!resIsInstallDrivers)
            {
                //设备是否安装驱动
                IsInstallDrivers = new StringBuilder("");
            }
            //释放当前设备占用内存
            SetupApi.SetupDiDestroyDeviceInfoList(NewDeviceInfoSet);
            return 0;
        }

        /// <summary>
        /// 获取未知设备信息
        /// </summary>
        /// <param name="DeviceIndex"></param>
        /// <param name="ClassName"></param>
        /// <param name="DeviceName"></param>
        /// <returns></returns>
        public static int EnumerateDevices(List<string> NameList, List<string> IDList, List<string> MfgList, List<string> TypeList, List<string> IsInstallDriversList)
        {
            Guid myGUID = System.Guid.Empty;
            IntPtr hDevInfo = SetupApi.SetupDiGetClassDevsA(ref myGUID, 0, IntPtr.Zero, SetupApi.DIGCF_ALLCLASSES);

            if (hDevInfo.ToInt32() == -1)
            {
                //设备不可用

                return -3;
            }
            SP_DEVINFO_DATA DeviceInfoData = new SP_DEVINFO_DATA();
            DeviceInfoData.cbSize = 28;
            //正常状态
            DeviceInfoData.DevInst = 0;
            DeviceInfoData.ClassGuid = System.Guid.Empty;
            DeviceInfoData.Reserved = 0;
            UInt32 i;
            for (i = 0; SetupApi.SetupDiEnumDeviceInfo(hDevInfo, i, DeviceInfoData); i++)
            {
                //设备名称
                StringBuilder DeviceName = new StringBuilder("");
                //设备ID
                StringBuilder DeviceID = new StringBuilder("");
                //设备供应商
                StringBuilder Mfg = new StringBuilder("");
                //设备类型
                StringBuilder DeviceType = new StringBuilder("");
                //设备类型
                StringBuilder IsInstallDrivers = new StringBuilder("");
                DeviceName.Capacity = SetupApi.MAX_DEV_LEN;
                DeviceID.Capacity = SetupApi.MAX_DEV_LEN;
                DeviceType.Capacity = SetupApi.MAX_DEV_LEN;
                Mfg.Capacity = SetupApi.MAX_DEV_LEN;
                IsInstallDrivers.Capacity = SetupApi.MAX_DEV_LEN;
                bool resName = SetupApi.SetupDiGetDeviceRegistryPropertyA(hDevInfo, DeviceInfoData, SetupApi.SPDRP_DEVICEDESC,
                    0, DeviceName, SetupApi.MAX_DEV_LEN, IntPtr.Zero);
                if (!resName)
                {
                    //设备名称未知
                    DeviceName = new StringBuilder("");
                }
                bool resClass = SetupApi.SetupDiGetDeviceRegistryPropertyA(hDevInfo, DeviceInfoData, SetupApi.SPDRP_CLASS,
                    0, DeviceType, SetupApi.MAX_DEV_LEN, IntPtr.Zero);
                if (!resClass)
                {
                    //设备类型未知
                    DeviceType = new StringBuilder("");
                }
                //设备ID
                bool resHardwareID = SetupApi.SetupDiGetDeviceRegistryPropertyA(hDevInfo,
                 DeviceInfoData, SetupApi.SPDRP_HARDWAREID, 0, DeviceID, SetupApi.MAX_DEV_LEN, IntPtr.Zero);
                if (!resHardwareID)
                {
                    //设备ID未知
                    DeviceID = new StringBuilder("");
                }

                //设备供应商
                bool resMfg = SetupApi.SetupDiGetDeviceRegistryPropertyA(hDevInfo,
                 DeviceInfoData, SetupApi.SPDRP_MFG, 0, Mfg, SetupApi.MAX_DEV_LEN, IntPtr.Zero);
                if (!resMfg)
                {
                    //设备供应商未知
                    Mfg = new StringBuilder("");
                }
                
                bool resIsInstallDrivers = SetupApi.SetupDiGetDeviceRegistryPropertyA(hDevInfo,
                 DeviceInfoData, SetupApi.SPDRP_DRIVER, 0, IsInstallDrivers, SetupApi.MAX_DEV_LEN, IntPtr.Zero);
                if (!resIsInstallDrivers)
                {
                    //设备是否安装驱动
                    IsInstallDrivers = new StringBuilder("");
                }

                if (string.IsNullOrEmpty(DeviceType.ToString()))
                {
                    if (!string.IsNullOrEmpty(DeviceName.ToString()) && !string.IsNullOrEmpty(DeviceID.ToString()))
                    {
                        TypeList.Add("其它设备");
                        NameList.Add(DeviceName.ToString());
                        IDList.Add(DeviceID.ToString());
                        MfgList.Add(Mfg.ToString());
                        IsInstallDriversList.Add(IsInstallDrivers.ToString());
                    }

                }
            }
            //释放当前设备占用内存
            SetupApi.SetupDiDestroyDeviceInfoList(hDevInfo);
            return 0;
        }
    }
}
