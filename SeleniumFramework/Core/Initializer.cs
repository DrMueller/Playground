using Microsoft.Practices.Unity;

namespace MMU.SeleniumExtensions.Core
{
    public static class Initializer
    {
        public static void Initialize(IUnityContainer unityContainer)
        {
            Infrastructure.Singletons.UnityContainerSingleton.Initialize(unityContainer);
        }
    }
}
