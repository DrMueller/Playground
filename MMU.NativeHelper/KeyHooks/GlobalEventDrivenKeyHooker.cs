namespace MMU.NativeHelper.KeyHooks
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using MMU.NativeHelper.Classes;
    using MMU.NativeHelper.Constants;
    using MMU.NativeHelper.Delegates;
    using MMU.NativeHelper.Helper;

    public class EventDrivenKeyHookHelper : IDisposable
    {
        private LowLevelKeyboardProc _hookedProc;
        private IntPtr _hookId = IntPtr.Zero;

        public EventDrivenKeyHookHelper()
        {
            _hookedProc = HookProc;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public event LowLevelKeyDownDelegate KeyDown;
        public event LowLevelKeyDownDelegate KeyUp;

        private void OnKeyDown(Keys key)
        {
            if (KeyDown != null)
                KeyDown(this, new KeyHandlerEventArgs(key));
        }

        private void OnkeyUp(Keys key)
        {
            if (KeyUp != null)
                KeyUp(this, new KeyHandlerEventArgs(key));
        }

        public void Hook()
        {
            _hookId = NativeMethods.SetWindowsHookEx(CommandConstants.WH_KEYBOARD_LL, _hookedProc, IntPtr.Zero, 0);
        }

        public void HookByAppDomain()
        {
            _hookId = NativeMethods.SetWindowsHookEx(CommandConstants.WH_KEYBOARD, _hookedProc, IntPtr.Zero, 0);
        }

        ~EventDrivenKeyHookHelper()
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
                var val = Marshal.ReadInt32(lParam);
                var key = (Keys)val;

                if (wParam.ToInt32() == CommandConstants.WM_KEYDOWN)
                {
                    OnKeyDown(key);
                }
                else if (wParam.ToInt32() == CommandConstants.WM_KEYUP)
                {
                    OnkeyUp(key);
                }
            }

            return (int)NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        private void Dispose(bool disposing)
        {
            UnHook();
            _hookedProc = null;
            _hookId = IntPtr.Zero;
        }
    }
}