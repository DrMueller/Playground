using Microsoft.Practices.Unity;
using MMU.TextFunctions.GUI.Container;

namespace MMU.TextFunctions.GUI.ViewModels.Shell
{
    public class ViewModelFactory
    {
        internal ViewModelBase CreateViewModel<T>()
            where T : ViewModelBase
        {
            var result = UnityInstance.Instance.Resolve<T>();
            return result;
        }
    }
}
