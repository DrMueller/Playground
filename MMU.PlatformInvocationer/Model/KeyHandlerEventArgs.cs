using System;
using System.Windows.Forms;

namespace MMU.PlatformInvocationer.Model
{
    public class KeyHandlerEventArgs : EventArgs
    {
        public KeyHandlerEventArgs(Keys key)
        {
            Key = key;
        }

        public Keys Key { get; private set; }
    }
}
