namespace MMU.NativeHelper.Classes
{
    using System;
    using System.Windows.Forms;

    public class KeyHandlerEventArgs : EventArgs
    {
        public KeyHandlerEventArgs(Keys key)
        {
            Key = key;
        }

        public Keys Key { get; set; }
    }
}