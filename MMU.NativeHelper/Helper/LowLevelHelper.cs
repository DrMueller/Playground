namespace MMU.NativeHelper.Helper
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    internal static class LowLevelHelper
    {
        internal static string GetLastWin32Message()
        {
            var errorMessage = new Win32Exception(Marshal.GetLastWin32Error()).Message;
            return errorMessage;
        }

        internal static IntPtr GetCurrentModuleHandle()
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return NativeMethods.GetModuleHandle(curModule.ModuleName);
            }
        }
    }
}