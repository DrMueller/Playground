using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMU.NativeHelper.Helper
{
    using MMU.NativeHelper.Delegates;
    using MMU.NativeHelper.Structs;

    internal class RecursiveClassHelper
    {
        private IntPtr _result;
        private string _className;

        internal IntPtr GetHandleToClassNameRecursive(IntPtr parent, string className)
        {
            _result =  NativeMethods.FindWindowEx(parent, IntPtr.Zero, className, null);
            _className = className;
            if (_result == IntPtr.Zero)
               NativeMethods.EnumChildWindows(parent, new Win32Callback2(FindClassCallback), IntPtr.Zero);
            return _result;
        }

        internal void EnumEverything(IntPtr parent)
        {
            NativeMethods.EnumChildWindows(parent, new Win32Callback2(GetDataCallback), IntPtr.Zero);
        }

        private bool GetDataCallback(IntPtr hwnd, IntPtr lParam)
        {
            StringBuilder sb = new StringBuilder(255);
            NativeMethods.GetWindowText(hwnd, sb, 255);

            STRINGBUFFER sbClassName;
            NativeMethods.GetClassName(hwnd, out sbClassName, 255);
            var title = new StringBuilder(255);
            NativeMethods.SendMessage(hwnd, Constants.CommandConstants.WM_GETTEXT, title.Capacity, title);
            System.Diagnostics.Debug.WriteLine("Handle: {0}, Class: {1}, Text: {2}, Text2: {3}",
                hwnd,
                sbClassName.Text,
                title.ToString(),
                sb.ToString()
            );

            //http://www.pinvoke.net/default.aspx/user32/GetClassInfoEx.html
            if (title.ToString() == "1234")
            {
                NativeMethods.SendMessage(hwnd, Constants.CommandConstants.WM_SETTEXT, IntPtr.Zero, string.Empty);
            }

            NativeMethods.EnumChildWindows(hwnd, new Win32Callback2(GetDataCallback), IntPtr.Zero);
            return true;
        }

        private bool FindClassCallback(IntPtr hwnd, IntPtr lParam)
        {
            _result = NativeMethods.FindWindowEx(hwnd, IntPtr.Zero, _className, null);
            if (_result != IntPtr.Zero)
                return false;
            else
                NativeMethods.EnumChildWindows(hwnd, new Win32Callback2(FindClassCallback), IntPtr.Zero);
            return true;
        }
    }
}
