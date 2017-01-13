using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMU.PlatformInvocationer.Native
{
    using MMU.PlatformInvocationer.Constants;
    using MMU.PlatformInvocationer.Model;

    internal static class NativeFunctionsBridge
    {
        internal static string GetClassNameByHandle(IntPtr handle)
        {
            StringBuffer bufferClassName;
            DllImports.GetClassName(handle, out bufferClassName, 255);

            return bufferClassName.Text;
        }


        internal static string GetTextByHandle(IntPtr handle)
        {
            Int32 textSze = DllImports.SendMessage((int)handle, CommandConstants.WM_GETTEXTLENGTH, 0, 0).ToInt32();

            if (textSze > 0)
            {
                var sb = new StringBuilder(textSze + 1);
                DllImports.SendMessage(handle, CommandConstants.WM_GETTEXT, sb.Capacity, sb);
                return sb.ToString();
            }

            return string.Empty;
        }
    }
}
