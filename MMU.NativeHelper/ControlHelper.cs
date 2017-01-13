using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMU.NativeHelper
{
    using System.Diagnostics;

    // http://stackoverflow.com/questions/15292175/c-sharp-using-sendkey-function-to-send-a-key-to-another-application
    public class ControlHelper
    {
        public void GetFocusedControl()
        {
            Control focusControl = null;
            IntPtr focusHandle = Helper.NativeMethods.GetFocus();
            if (focusHandle != IntPtr.Zero)
                focusControl = Control.FromHandle(focusHandle);

            string str;

            if (focusControl.Name.ToString().Length == 0)
                str = focusControl.Parent.Parent.Name.ToString();
            else
                str = focusControl.Name.ToString();

            Debug.WriteLine(str);
        }
    }
}
