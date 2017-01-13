using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MMU.PlatformInvocationer.Helper
{
    internal static class ActionHandler
    {
        internal static void HandledInvokeAction(Action action)
        {
            action();
            CheckThrowWin32Exception();
        }

        internal static T HandledInvokeFunction<T>(Func<T> func)
        {
            T result = func();
            CheckThrowWin32Exception();

            return result;
        }

        private static void CheckThrowWin32Exception()
        {
            Win32Exception win32Exception;
            if (TryGettingLastWin32Exception(out win32Exception))
            {
                throw win32Exception;
            }
        }

        private static bool TryGettingLastWin32Exception(out Win32Exception win32Exception)
        {
            var lastWin32ExceptionId = Marshal.GetLastWin32Error();

            if (lastWin32ExceptionId > 0)
            {
                win32Exception = new Win32Exception(lastWin32ExceptionId);
                return true;
            }

            win32Exception = null;
            return false;
        }
    }
}
