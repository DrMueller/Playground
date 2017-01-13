using MMU.SeleniumExtensions.Core.Infrastructure.Singletons;
using MMU.SeleniumExtensions.Core.Models.Enumerations;
using OpenQA.Selenium.IE;
using System;
using Microsoft.Practices.Unity;
using OpenQA.Selenium.Remote;

namespace MMU.SeleniumExtensions.Core.Logics.Factories
{
    public class RemoteWebDriverFactory
    {
        internal RemoteWebDriver Create(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.InternetExplorer:
                    {

                        var driverService = InternetExplorerDriverService.CreateDefaultService();
                        driverService.HideCommandPromptWindow = true;

                        var opt = new InternetExplorerOptions();
                        opt.IgnoreZoomLevel = true;
                        opt.RequireWindowFocus = false;

                        var parameterOverrides = new ParameterOverrides();
                        parameterOverrides.Add("service", driverService);
                        parameterOverrides.Add("options", opt);

                        var result = UnityContainerSingleton.Instance.Resolve<InternetExplorerDriver>(parameterOverrides);
                        return result;
                    }
                default:
                    {
                        throw new NotImplementedException(browserType.ToString());
                    }
            }
        }
    }
}
