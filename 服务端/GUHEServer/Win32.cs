using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.InteropServices;

namespace GHIBMS.Server
{
    /// <summary>
    /// Class1 的摘要说明。
    /// </summary>
    public class Win32
    {
        public const int WM_DEVICECHANGE = 0x0219;
        public const int DBT_DEVICEARRIVAL = 0x8000, // systemdetected a new device
                           DBT_DEVICEREMOVECOMPLETE = 0x8004; // device is gone
        public const int DEVICE_NOTIFY_WINDOW_HANDLE = 0,
            DEVICE_NOTIFY_SERVICE_HANDLE = 1;
        public const int DBT_DEVTYP_DEVICEINTERFACE = 0x00000005; // deviceinterface class
        public static Guid GUID_DEVINTERFACE_USB_DEVICE = new
            Guid("A5DCBF10-6530-11D2-901F-00C04FB951ED");

        [StructLayout(LayoutKind.Sequential)]
        public class DEV_BROADCAST_HDR
        {
            public int dbcc_size;
            public int dbcc_devicetype;
            public int dbcc_reserved;
        }
        [StructLayout(LayoutKind.Sequential)]
        public class
            DEV_BROADCAST_DEVICEINTERFACE
        {
            public int dbcc_size;
            public int dbcc_devicetype;
            public int dbcc_reserved;
            public Guid dbcc_classguid;
            public short dbcc_name;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class DEV_BROADCAST_DEVICEINTERFACE1
        {
            public int dbcc_size;
            public int dbcc_devicetype;
            public int dbcc_reserved;
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1,
                SizeConst = 16)]
            public byte[] dbcc_classguid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            public char[]
                dbcc_name;
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern
            IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr
            NotificationFilter, Int32 Flags);
        [DllImport("kernel32.dll")]
        public static extern int GetLastError();
    }


}
