namespace MMU.PlatformInvocationer.Model
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct StringBuffer
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string Text;
    }
}