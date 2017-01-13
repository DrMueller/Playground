using System;

namespace MMU.PlatformInvocationer.Model
{
    public class NativeControl
    {
        public string Text { get; set; }

        public IntPtr Handle { get; set; }

        public string ClassName { get; set; }

        public override string ToString()
        {
            return $"Class: {ClassName}, Handle: {Handle}, Text: {Text}";
        }
    }
}
