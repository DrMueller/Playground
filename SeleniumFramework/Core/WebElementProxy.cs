using MMU.SeleniumExtensions.Core.Services;
using OpenQA.Selenium;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace MMU.SeleniumExtensions.Core
{
    public class WebElementProxy
    {
        private readonly IWebElement _webElement;
        private readonly SearchContextProxyService _searchContextProxyService;

        internal IWebElement WebElement
        {
            get
            {
                return _webElement;
            }
        }

        public string Text
        {
            get
            {
                return _webElement.Text;
            }
        }

        public WebElementProxy(IWebElement webElement)
        {
            _searchContextProxyService = Infrastructure.Singletons.UnityContainerSingleton.Instance.Resolve<SearchContextProxyService>();
            _searchContextProxyService.Initialize(webElement);
            _webElement = webElement;
        }

        public void Click()
        {
            _webElement.Click();
        }

        public void SendKeys(string text)
        {
            _webElement.SendKeys(text);
        }

        public string GetAttribute(string attributeName)
        {
            return _webElement.GetAttribute(attributeName);
        }

        public WebElementProxy FindElement(ByProxy byProxy)
        {
            return _searchContextProxyService.FindElement(byProxy.By);
        }

        public IReadOnlyCollection<WebElementProxy> FindElements(ByProxy byProxy)
        {
            return _searchContextProxyService.FindElements(byProxy.By);
        }
    }
}
