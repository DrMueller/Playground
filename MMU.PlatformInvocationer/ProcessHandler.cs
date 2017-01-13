using MMU.PlatformInvocationer.Helper;
using System;

namespace MMU.PlatformInvocationer
{
    using System.Diagnostics;
    using MMU.PlatformInvocationer.Native;

    public class ProcessHandler
    {
        public IntPtr GetWindowHandleByForegroundWindow()
        {
            return ActionHandler.HandledInvokeFunction(DllImports.GetForegroundWindow);
        }

        public IntPtr GetMainWowHandleByProcessName(string procName)
        {
            var process = Process.GetProcessesByName(procName)[0];
            return process.MainWindowHandle;
        }

        private IntPtr GetProcessHandleByProcessId(int procId)
        {
            return DllImports.OpenProcess(Constants.CommandConstants.PROCESS_WM_READ, false, procId);
        }
    }
}
