namespace MMU.NativeHelper
{
    using System.Security.Principal;

    public static class SecurityHelper
    {
        public static bool CheckIfCurrentProcessIsRunningAsAdmin()
        {
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);
            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}