namespace MMU.NativeHelper.KeyHooks
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using MMU.NativeHelper.Classes;
    using MMU.NativeHelper.Constants;
    using MMU.NativeHelper.Delegates;
    using MMU.NativeHelper.Helper;
    using MMU.NativeHelper.Structs;

    public class GlobalKeyHookHelper : IDisposable
    {
        private readonly LowLevelKeyboardProc _hookedProc;
        private readonly HookingObjectsList _hookingObjects;
        private IntPtr _hookId = IntPtr.Zero;

        public GlobalKeyHookHelper()
        {
            _hookingObjects = new HookingObjectsList();
            _hookedProc = HookProc;
            Hook();
        }

        public void AddHookedKey(HookingObject obj)
        {
            _hookingObjects.Add(obj);
        }

        public void Hook()
        {

            _hookId = NativeMethods.SetWindowsHookEx(CommandConstants.WH_KEYBOARD, _hookedProc, IntPtr.Zero, 0);
            //var t = LowLevelHelper.GetLastWin32Message();
            //Debug.WriteLine(t);
        }

        ~GlobalKeyHookHelper()
        {
            Dispose(false);
        }

        public void UnHook()
        {
            NativeMethods.UnhookWindowsHookEx(_hookId);
        }

        private int HookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int val = Marshal.ReadInt32(lParam);
                var key = (Keys)val;
                Debug.WriteLine(key);
            }

            return (int)NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        private HookingEvent GetHookingEventFromWindowsEvent(int windowsInt)
        {
            switch (windowsInt)
            {
                case CommandConstants.WM_KEYDOWN:
                    return HookingEvent.KeyDown;
                case CommandConstants.WM_KEYUP:
                    return HookingEvent.KeyUp;
                default:
                    return HookingEvent.None;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            UnHook();
            _hookId = IntPtr.Zero;
        }
    }
}