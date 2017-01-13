namespace MMU.NativeHelper.Classes
{
    using System;
    using System.Windows.Forms;
    using Structs;

    public class HookingObject
    {
        public Keys HookedKey { get; set; }
        public HookingEvent HandlingEvent { get; set; }
        public Action Callback { get; set; }
        public bool BubbleEvent { get; set; }
    }
}