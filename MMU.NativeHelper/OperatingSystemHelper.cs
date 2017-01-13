namespace MMU.NativeHelper
{
    using Helper;

    public class OperatingSystemHelper
    {
        public void LockWorkstation()
        {
            NativeMethods.LockWorkStation();
        }
    }
}