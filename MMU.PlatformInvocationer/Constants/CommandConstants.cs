namespace MMU.PlatformInvocationer.Constants
{
    internal static class CommandConstants
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644990%28v=vs.85%29.aspx
        //KeyDown
        internal const int WH_KEYBOARD_LL = 13;
        internal const int WH_KEYBOARD = 2;

        internal const int WM_KEYDOWN = 0x100;
        internal const int WM_KEYUP = 0x101;
        internal const int WM_SYSKEYDOWN = 0x104;
        internal const int WM_SYSKEYUP = 0x105;

        //Process
        internal const int PROCESS_WM_READ = 0x0010;

        //SendMessage
        internal const int WM_SETTEXT = 0x000C;
        internal const int WM_GETTEXT = 0xD;
        internal const int WM_GETTEXTLENGTH = 0x000E;
    }
}
