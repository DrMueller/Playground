namespace MMU.PlatformInvocationer.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using MMU.PlatformInvocationer.Model;
    using MMU.PlatformInvocationer.Native;

    internal class RecursiveControlEnumerationHelper
    {
        private List<NativeControl> _controls;

        internal IEnumerable<NativeControl> GetRecursiveControlsByWindowHandle(IntPtr windowHandle)
        {
            _controls = new List<NativeControl>();
            DllImports.EnumChildWindows(windowHandle, GetDataCallback, IntPtr.Zero);
            return _controls;
        }

        private bool GetDataCallback(IntPtr hwnd, IntPtr lParam)
        {
            var nativeControl = new NativeControl
            {
                Text = NativeFunctionsBridge.GetTextByHandle(hwnd),
                ClassName = NativeFunctionsBridge.GetClassNameByHandle(hwnd),
                Handle = hwnd
            };

            if (nativeControl.Text == "z1234")
            {
                WndClassEx cls = WndClassEx.Build();
                var classEx = DllImports.GetClassInfoEx(hwnd, nativeControl.ClassName, ref cls);
                Debug.WriteLine(1);
            }

            Debug.WriteLine(nativeControl);
            _controls.Add(nativeControl);

            DllImports.EnumChildWindows(hwnd, GetDataCallback, IntPtr.Zero);
            return true;
        }
    }
}