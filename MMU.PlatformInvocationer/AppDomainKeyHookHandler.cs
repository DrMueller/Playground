namespace MMU.PlatformInvocationer
{
    using System;
    using System.Windows.Forms;
    using MMU.PlatformInvocationer.Constants;
    using MMU.PlatformInvocationer.Delegates;
    using MMU.PlatformInvocationer.Helper;
    using MMU.PlatformInvocationer.Model;
    using MMU.PlatformInvocationer.Native;

    public class AppDomainKeyHookHandler : IDisposable
    {
        private LowLevelKeyboardProc _hookedProc;
        private IntPtr _hookId = IntPtr.Zero;

        public void Dispose()
        {
            Dispose(true);
        }

        public event KeyDownDelegate KeyDown;

        private void OnKeyDown(Keys key)
        {
            if (KeyDown != null)
                KeyDown(this, new KeyHandlerEventArgs(key));
        }

        public void Hook()
        {
            ActionHandler.HandledInvokeAction(
                () =>
                {
                    _hookId = DllImports.SetWindowsHookEx(CommandConstants.WH_KEYBOARD, _hookedProc, IntPtr.Zero,
                        (uint)AppDomain.GetCurrentThreadId());
                });
        }

        ~AppDomainKeyHookHandler()
        {
            Dispose(false);
        }

        public void UnHook()
        {
            ActionHandler.HandledInvokeAction(() => {DllImports.UnhookWindowsHookEx(_hookId);});
        }

        private int HookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            return ActionHandler.HandledInvokeFunction(() =>
            {
                if (nCode >= 0)
                {
                    var key = (Keys)wParam;
                    OnKeyDown(key);
                }

                return (int)DllImports.CallNextHookEx(_hookId, nCode, wParam, lParam);
            });
        }

        private void Dispose(bool disposing)
        {
            ActionHandler.HandledInvokeAction(() =>
            {
                UnHook();
                _hookedProc = null;
                _hookId = IntPtr.Zero;
            });
        }
    }
}