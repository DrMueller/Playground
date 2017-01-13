namespace MMU.NativeHelper.Classes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    public class HookingObjectsList
    {
        private readonly List<HookingObject> _objects;

        public HookingObjectsList()
        {
            _objects = new List<HookingObject>();
        }

        public HookingObject this[Keys key]
        {
            get { return _objects.FirstOrDefault(f => f.HookedKey == key); }
        }

        public void Add(HookingObject hookingObject)
        {
            _objects.Add(hookingObject);
        }
    }
}