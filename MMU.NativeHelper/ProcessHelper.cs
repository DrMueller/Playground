namespace MMU.NativeHelper
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using MMU.NativeHelper.Constants;
    using MMU.NativeHelper.Helper;

    public class ProcessHelper
    {
        public IntPtr GetProcessHandleByProcessId(int procId)
        {
            return NativeMethods.OpenProcess(CommandConstants.PROCESS_WM_READ, false, procId);
        }

        [Conditional("DEBUG")]
        public void EnumProcessNames()
        {
            foreach (var proc in Process.GetProcesses())
            {
                Debug.WriteLine(proc.ProcessName);
            }
        }

        public IntPtr GetProcessHandleByProcessName(string procName)
        {
            var process = Process.GetProcessesByName(procName)[0];
            return GetProcessHandleByProcessId(process.Id);
        }

        public string ReadProcessMemory(IntPtr procHandle, IntPtr position)
        {
            var result = string.Empty;
            var bytesRead = new IntPtr();
            var buffer = new byte[255];

            NativeMethods.ReadProcessMemory(procHandle, position, buffer, new IntPtr(buffer.Length), out bytesRead);

            result = Encoding.Unicode.GetString(buffer);
            if (!string.IsNullOrEmpty(result) && result != "\0\0\0\0\0\0\0\0\0\0\0\0")
            {
                Debug.WriteLine("Unicode: " + result);
            }

            result = Encoding.ASCII.GetString(buffer);
            if (!string.IsNullOrEmpty(result) && result != "\0\0\0\0\0\0\0\0\0\0\0\0")
            {
                Debug.WriteLine("ASCII: " + result);
            }

            result = Encoding.UTF8.GetString(buffer);
            if (!string.IsNullOrEmpty(result) && result != "\0\0\0\0\0\0\0\0\0\0\0\0")
            {
                Debug.WriteLine("UTF8: " + result);
            }

            return result;
        }

        public Process GetProcessByForegroundWindow()
        {
            var hWnd = NativeMethods.GetForegroundWindow();
            uint procId = 0;
            NativeMethods.GetWindowThreadProcessId(hWnd, out procId);
            var proc = Process.GetProcessById((int)procId);
            return proc;
        }
    }
}