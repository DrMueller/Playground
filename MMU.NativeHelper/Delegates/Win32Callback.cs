namespace MMU.NativeHelper.Delegates
{
    using System;
    using Classes;

    internal delegate bool Win32Callback(IntPtr hwnd, ref EnumWinObj obj);
}