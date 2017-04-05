using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OgarCommon.Win32Api
{
    /// <summary>
    /// 定义了一个设备实例就是一个设备信息集合的成员
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class SP_DEVINFO_DATA
    {
        public int cbSize;
        public Guid ClassGuid;
        public int DevInst;
        public ulong Reserved;
    }


    /// <summary>
    /// 设备类型图标信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class SP_CLASSIMAGELIST_DATA
    {
        public int cbSize;
        public string ImageList;
        public ulong Reserved;
    }


    public struct RECT
    {
        long left;
        long top;
        long right;
        long bottom;
    }


    public class SetupApi
    {
        public const int DMI_MASK = 0x00000001;
        public const int DMI_BKCOLOR = 0x00000002;
        public const int DMI_USERECT = 0x00000004;

        public const int DIGCF_ALLCLASSES = (0x00000004);
        public const int DIGCF_PRESENT = (0x00000002);
        public const int DIOCR_INSTALLER = 0x00000001;
        public const int MAXIMUM_ALLOWED = 0x02000000;

        public const int MAX_DEV_LEN = 1000;//返回值最大长度

        public const int SPDRP_DEVICEDESC = (0x00000000);// DeviceDesc (R/W)
        public const int SPDRP_HARDWAREID = (0x00000001);// HardwareID (R/W)
        public const int SPDRP_COMPATIBLEIDS = (0x00000002);// CompatibleIDs (R/W)
        public const int SPDRP_UNUSED0 = (0x00000003);// unused
        public const int SPDRP_SERVICE = (0x00000004);// Service (R/W)
        public const int SPDRP_UNUSED1 = (0x00000005);// unused
        public const int SPDRP_UNUSED2 = (0x00000006);// unused
        public const int SPDRP_CLASS = (0x00000007);// Class (R--tied to ClassGUID)
        public const int SPDRP_CLASSGUID = (0x00000008);// ClassGUID (R/W)
        public const int SPDRP_DRIVER = (0x00000009);// Driver (R/W)
        public const int SPDRP_CONFIGFLAGS = (0x0000000A);// ConfigFlags (R/W)
        public const int SPDRP_MFG = (0x0000000B);// Mfg (R/W)
        public const int SPDRP_FRIENDLYNAME = (0x0000000C);// FriendlyName (R/W)
        public const int SPDRP_LOCATION_INFORMATION = (0x0000000D);// LocationInformation (R/W)
        public const int SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = (0x0000000E);// PhysicalDeviceObjectName (R)
        public const int SPDRP_CAPABILITIES = (0x0000000F);// Capabilities (R)
        public const int SPDRP_UI_NUMBER = (0x00000010);// UiNumber (R)
        public const int SPDRP_UPPERFILTERS = (0x00000011);// UpperFilters (R/W)
        public const int SPDRP_LOWERFILTERS = (0x00000012);// LowerFilters (R/W)
        public const int SPDRP_BUSTYPEGUID = (0x00000013);// BusTypeGUID (R)
        public const int SPDRP_LEGACYBUSTYPE = (0x00000014);// LegacyBusType (R)
        public const int SPDRP_BUSNUMBER = (0x00000015);// BusNumber (R)
        public const int SPDRP_ENUMERATOR_NAME = (0x00000016);// Enumerator Name (R)
        public const int SPDRP_SECURITY = (0x00000017);// Security (R/W, binary form)
        public const int SPDRP_SECURITY_SDS = (0x00000018);// Security=(W, SDS form)
        public const int SPDRP_DEVTYPE = (0x00000019);// Device Type (R/W)
        public const int SPDRP_EXCLUSIVE = (0x0000001A);// Device is exclusive-access (R/W)
        public const int SPDRP_CHARACTERISTICS = (0x0000001B);// Device Characteristics (R/W)
        public const int SPDRP_ADDRESS = (0x0000001C);// Device Address (R)
        public const int SPDRP_UI_NUMBER_DESC_FORMAT = (0X0000001D);// UiNumberDescFormat (R/W)
        public const int SPDRP_DEVICE_POWER_DATA = (0x0000001E);// Device Power Data (R)
        public const int SPDRP_REMOVAL_POLICY = (0x0000001F);// Removal Policy (R)
        public const int SPDRP_REMOVAL_POLICY_HW_DEFAULT = (0x00000020);// Hardware Removal Policy (R)
        public const int SPDRP_REMOVAL_POLICY_OVERRIDE = (0x00000021);// Removal Policy Override (RW)
        public const int SPDRP_INSTALL_STATE = (0x00000022);// Device Install State (R)
        public const int SPDRP_LOCATION_PATHS = (0x00000023);// Device Location Paths (R)
        public const int SPDRP_BASE_CONTAINERID = (0x00000024);// Base ContainerID (R)
        public const int SPDRP_MAXIMUM_PROPERTY = (0x00000025);// Upper bound on ordinals

        /// <summary>
        /// 检索与类GUID相关联的类名
        /// </summary>
        /// <param name="ClassGuid"></param>
        /// <param name="ClassName"></param>
        /// <param name="ClassNameSize"></param>
        /// <param name="RequiredSize"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll")]
        public static extern Boolean SetupDiClassNameFromGuidA(ref Guid ClassGuid, StringBuilder ClassName, UInt32 ClassNameSize, ref UInt32 RequiredSize);

        /// <summary>
        /// 获取一个指定类别或全部类别的所有已安装设备的信息
        /// </summary>
        /// <param name="ClassGuid"></param>
        /// <param name="Enumerator"></param>
        /// <param name="hwndParent"></param>
        /// <param name="Flags"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll")]
        public static extern IntPtr SetupDiGetClassDevsA(ref Guid ClassGuid, UInt32 Enumerator, IntPtr hwndParent, UInt32 Flags);

        /// <summary>
        /// 销毁一个设备信息集合,并且释放所有关联的内存
        /// </summary>
        /// <param name="DeviceInfoSet"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll")]
        public static extern Boolean SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        /// <summary>
        /// 打开设备安装程序类的注册表项，设备接口类的注册表项，或一个特定类的子项。此函数打开本地计算机或远程计算机上指定的键。
        /// </summary>
        /// <param name="ClassGuid"></param>
        /// <param name="samDesired"></param>
        /// <param name="Flags"></param>
        /// <param name="MachineName"></param>
        /// <param name="Reserved"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll")]
        public static extern IntPtr SetupDiOpenClassRegKeyExA(ref Guid ClassGuid, UInt32 samDesired, int Flags, IntPtr MachineName, UInt32 Reserved);

        /// <summary>
        /// 获取设备信息集合的设备信息元素。
        /// </summary>
        /// <param name="DeviceInfoSet"></param>
        /// <param name="MemberIndex"></param>
        /// <param name="DeviceInfoData"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll")]
        public static extern Boolean SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, UInt32 MemberIndex, SP_DEVINFO_DATA DeviceInfoData);


        /// <summary>
        /// 获取图标
        /// </summary>
        /// <param name="ClassImageListData"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern Boolean SetupDiGetClassImageList(out SP_CLASSIMAGELIST_DATA ClassImageListData);

        /// <summary>
        /// 绘制小图标
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="rc"></param>
        /// <param name="MiniIconIndex"></param>
        /// <param name="Flags"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll")]
        public static extern int SetupDiDrawMiniIcon(Graphics hdc, RECT rc, int MiniIconIndex, int Flags);

        /// <summary>
        /// 检索指定类提供的小图标的索引。
        /// </summary>
        /// <param name="ClassGuid"></param>
        /// <param name="MiniIconIndex"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll")]
        public static extern bool SetupDiGetClassBitmapIndex(Guid ClassGuid, out int MiniIconIndex);

        /// <summary>
        /// 加载小图标
        /// </summary>
        /// <param name="classGuid"></param>
        /// <param name="hIcon"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll")]
        public static extern int SetupDiLoadClassIcon(ref Guid classGuid, out IntPtr hIcon, out int index);


        [DllImport("setupapi.dll")]
        public static extern Boolean SetupDiClassGuidsFromNameA(string ClassN, ref Guid guids, UInt32 ClassNameSize, ref UInt32 ReqSize);

        [DllImport("setupapi.dll")]
        public static extern Boolean SetupDiGetDeviceRegistryPropertyA(IntPtr DeviceInfoSet, SP_DEVINFO_DATA DeviceInfoData, UInt32 Property, UInt32 PropertyRegDataType, StringBuilder PropertyBuffer, UInt32 PropertyBufferSize, IntPtr RequiredSize);
    }
}
