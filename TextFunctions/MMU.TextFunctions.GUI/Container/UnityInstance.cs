using Microsoft.Practices.Unity;

namespace MMU.TextFunctions.GUI.Container
{
    internal static class UnityInstance
    {
        internal static UnityContainer Instance { get; private set; }

        internal static void SetUp(UnityContainer unityContainer)
        {
            Instance = unityContainer;
        }
    }
}
