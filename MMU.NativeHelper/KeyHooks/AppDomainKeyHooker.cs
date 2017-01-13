namespace MMU.NativeHelper.KeyHooks
{
    using System;
    using System.Windows.Forms;
    using MMU.NativeHelper.Classes;
    using MMU.NativeHelper.Constants;
    using MMU.NativeHelper.Delegates;
    using MMU.NativeHelper.Helper;

    public class AppDomainKeyHooker
    {
        private LowLevelKeyboardProc _hookedProc;
        private IntPtr _hookId = IntPtr.Zero;

        public AppDomainKeyHooker()
        {
            _hookedProc = HookProc;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public event LowLevelKeyDownDelegate KeyDown;

        private void OnKeyDown(Keys key)
        {
            if (KeyDown != null)
                KeyDown(this, new KeyHandlerEventArgs(key));
        }

        public void Hook()
        {
            _hookId = NativeMethods.SetWindowsHookEx(CommandConstants.WH_KEYBOARD, _hookedProc, IntPtr.Zero,
                (uint)AppDomain.GetCurrentThreadId());
        }

        ~AppDomainKeyHooker()
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
                var key = (Keys)wParam;
                OnKeyDown(key);
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