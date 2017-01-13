using MMU.NativeHelper.Classes;
using MMU.NativeHelper.Constants;
using MMU.NativeHelper.Delegates;
using MMU.NativeHelper.Helper;
using MMU.NativeHelper.Structs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMU.NativeHelper
{
    public class KeyHookHelper
    {
        private static IntPtr _hookId = IntPtr.Zero;
        private readonly HookingObjectsList _hookingObjects;
        private readonly KeyboardHookProc _hookedProc;

        public void AddHookedKey(HookingObject obj)
        {
            _hookingObjects.Add(obj);
        }


        public KeyHookHelper()
        {
            _hookingObjects = new HookingObjectsList();
            _hookedProc = new KeyboardHookProc(HookProc);
            Hook();
        }

        public void Hook()
        {
            IntPtr hInstance = DllImports.LoadLibrary("User32");
            _hookId = DllImports.SetWindowsHookEx(CommandConstants.WH_KEYBOARD, _hookedProc, hInstance, 0);
        }

        public void UnHook()
        {
            DllImports.UnhookWindowsHookEx(_hookId);
        }

        private int HookProc(int code, int wParam, ref KeyboardHookStruct lParam)
        {
            if (code >= 0)
            {
                Keys key = (Keys)lParam.vkCode;
                var obj = _hookingObjects[key];

                if (obj != null)
                {
                    var ev = GetHookingEventFromWindowsEvent(wParam);
                    if (obj.HandlingEvent == ev)
                    {
                        obj.Callback();
                        if (!obj.BubbleEvent)
                        { 
                            return 1;
                        }
                    }
                }
            }


            Debug.WriteLine(1);

            return DllImports.CallNextHookEx(_hookId, code, wParam, ref lParam);
        }

        private HookingEvent GetHookingEventFromWindowsEvent(int windowsInt)
        {
            switch (windowsInt)
            {
                case CommandConstants.WM_KEYDOWN:
                    return HookingEvent.KeyDown;
                case CommandConstants.WM_KEYUP:
                    return HookingEvent.KeyUp;
                default:
                    return HookingEvent.None;
            }
        }
    }
}
