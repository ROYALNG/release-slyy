using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace GHIBMS.NHikPlayer
{
    /// <summary>
    /// Win32系统方法
    /// </summary>
    public sealed class Win32API
    {
        /// <summary>
        /// 释放托管内容
        /// </summary>
        /// <param name="Destination"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", EntryPoint = "RtlZeroMemory", SetLastError = false)]
        public static extern void ZeroMemory(IntPtr Destination, IntPtr Length);

        /// <summary>
        /// 获取系统屏幕尺度
        /// </summary>
        /// <param name="nIndex">0表示宽度，1表示高度</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        /// <summary>
        /// 强制立即更新窗口，窗口中以前屏蔽的所有区域都会重绘
        /// </summary>
        /// <param name="hwnd">欲更新窗口的句柄</param>
        /// <returns>非零表示成功，零表示失败</returns>
        [DllImport("user32.dll", EntryPoint = "UpdateWindow", SetLastError = true,
            CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern long UpdateWindow(IntPtr hwnd);

        /// <summary>
        /// 获取显示区域
        /// </summary>
        /// <param name="hwnd">句柄</param>
        /// <param name="lpRect">返回的显示区域</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, ref Rectangle lpRect);
    }
}
