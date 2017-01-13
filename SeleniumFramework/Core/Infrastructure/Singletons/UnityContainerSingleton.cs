using System;
using Microsoft.Practices.Unity;
using OpenQA.Selenium.IE;

namespace MMU.SeleniumExtensions.Core.Infrastructure.Singletons
{
    public static class UnityContainerSingleton
    {
        private static IUnityContainer _instance;

        public static IUnityContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UnityContainer();
                    InitializeTypes();
                }

                return _instance;
            }
        }

        private static void InitializeTypes()
        {
            _instance.RegisterType<InternetExplorerDriver>(new InjectionConstructor());
        }

        public static void Initialize(IUnityContainer unityContainer)
        {
            _instance = unityContainer;
            InitializeTypes();
        }
    }
}
