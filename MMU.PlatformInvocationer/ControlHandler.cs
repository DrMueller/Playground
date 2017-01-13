namespace MMU.PlatformInvocationer
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MMU.PlatformInvocationer.Constants;
    using MMU.PlatformInvocationer.Helper;
    using MMU.PlatformInvocationer.Model;
    using MMU.PlatformInvocationer.Native;

    public class ControlHandler
    {
        public IEnumerable<NativeControl> GetAllControlsByWindowHandle(IntPtr windowHandle)
        {
            return ActionHandler.HandledInvokeFunction(() =>
            {
                var recursiveControlHelper = new RecursiveControlEnumerationHelper();
                return recursiveControlHelper.GetRecursiveControlsByWindowHandle(windowHandle);
            });
        }

        public void SetControlText(IntPtr handle, string text)
        {
            ActionHandler.HandledInvokeAction(
                () =>
                {
                    DllImports.SendMessage(handle, CommandConstants.WM_SETTEXT, text.Length, new StringBuilder(text));
                });
        }
        
        public NativeControl GetControlByHandle(IntPtr controlHandle)
        {
            return ActionHandler.HandledInvokeFunction(() =>
            {
                var nc = new NativeControl()
                {
                    Handle = controlHandle,
                    ClassName = NativeFunctionsBridge.GetClassNameByHandle(controlHandle),
                    Text = NativeFunctionsBridge.GetTextByHandle(controlHandle)
                };

                return nc;
            });
        }
    }
}