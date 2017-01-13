namespace MMU.NativeHelper
{
    using System;
    using System.Text;
    using MMU.NativeHelper.Classes;
    using MMU.NativeHelper.Delegates;
    using MMU.NativeHelper.Helper;
    using MMU.NativeHelper.Structs;

    public class WindowHelper
    {
        private Win32Callback _callback;

        public RECT GetWindowByTitle(string title)
        {
            var result = new RECT();
            var obj = new EnumWinObj();
            obj.WindowTitle = title;
            _callback = EnumWindowsCallback;
            NativeMethods.EnumWindows(_callback, ref obj);
            if (obj.WindowHandle != IntPtr.Zero)
                NativeMethods.GetWindowRect(obj.WindowHandle, out result);
            return result;
        }

        private bool EnumWindowsCallback(IntPtr hwnd, ref EnumWinObj obj)
        {
            var sb = new StringBuilder(255);
            NativeMethods.GetWindowText(hwnd, sb, 255);

            if (sb.ToString() == "Hearthstone")
            {
                obj.WindowHandle = hwnd;
                return false;
            }
            return true;
        }

        public string GetActiveWindowTitle()
        {
            const int nChars = 256;
            var buff = new StringBuilder(nChars);
            var handle = NativeMethods.GetForegroundWindow();

            if (NativeMethods.GetWindowText(handle, buff, nChars) > 0)
            {
                return buff.ToString();
            }

            return null;
        }

        public void EnumEverything()
        {
            IntPtr windowHandle = NativeMethods.GetForegroundWindow();
            RecursiveClassHelper r = new RecursiveClassHelper();
            r.EnumEverything(windowHandle);
        }

    }
}