using MMU.SeleniumExtensions.Core.Infrastructure.Singletons;
using MMU.SeleniumExtensions.Core.Logics.Factories;
using MMU.SeleniumExtensions.Core.Models.Enumerations;
using MMU.SeleniumExtensions.Core.Services;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using System.Linq;

namespace MMU.SeleniumExtensions.Core
{
    public class RemoteWebDriverProxy
    {
        private SearchContextProxyService _searchContextProxyService;
        private RemoteWebDriver _remoteWebDriver;

        public bool ThrowExceptionWhenElementNotFound
        {
            get
            {
                return _searchContextProxyService.ThrowExceptionWhenElementNotFound;
            }
            set
            {
                _searchContextProxyService.ThrowExceptionWhenElementNotFound = value;
            }
        }

        public event Infrastructure.Delegates.InformationPublishedEventHandler InformationPublished
        {
            add
            {
                _searchContextProxyService.InformationPublished += value;
            }
            remove
            {
                _searchContextProxyService.InformationPublished -= value;
            }
        }

        public RemoteWebDriverProxy(SearchContextProxyService service)
        {
            _searchContextProxyService = service;
        }

        public void Initialize(BrowserType browserType)
        {
            var factory = UnityContainerSingleton.Instance.Resolve<RemoteWebDriverFactory>();
            _remoteWebDriver = factory.Create(browserType);
            _searchContextProxyService.Initialize(_remoteWebDriver);
        }

        public void NavigateTo(string url)
        {
            _remoteWebDriver.Url = url;
        }

        public WebElementProxy FindElement(ByProxy byProxy)
        {
            return _searchContextProxyService.FindElement(byProxy.By);
        }

        public IReadOnlyCollection<WebElementProxy> FindElements(ByProxy byProxy)
        {
            return _searchContextProxyService.FindElements(byProxy.By);
        }

        public bool TryFindElement(ByProxy byProxy, out WebElementProxy webElementProxy)
        {
            return _searchContextProxyService.TryFindElementBy(byProxy.By, out webElementProxy);
        }

        public object ExecuteScript(string javaScript, params WebElementProxy[] args)
        {
            var mappedParameters = args.Select(f => f.WebElement).ToList();
            var result = _remoteWebDriver.ExecuteScript(javaScript, mappedParameters);
            return result;
        }
    }
}
