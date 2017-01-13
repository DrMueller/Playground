using MMU.SeleniumExtensions.Core.Models;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MMU.SeleniumExtensions.Core.Services
{
    public class SearchContextProxyService
    {
        private ISearchContext _searchContext;
        internal bool ThrowExceptionWhenElementNotFound { get; set; }
        internal event Infrastructure.Delegates.InformationPublishedEventHandler InformationPublished;

        internal void Initialize(ISearchContext searchContext)
        {
            _searchContext = searchContext;
        }

        internal bool TryFindElementBy(By by, out WebElementProxy webElementPrxy)
        {
            IWebElement element = null;
            try
            {
                OnNewInformation("Try finding " + by.ToString());
                element = _searchContext.FindElement(by);
                OnNewInformation($"Found Element { element?.Text ?? "(NULL)" }");
                webElementPrxy = new WebElementProxy(element);
                return true;
            }
            catch (NoSuchElementException)
            {
                OnNewInformation($"Element not found. By: { by }");
                webElementPrxy = null;
                return false;
            }
        }

        internal WebElementProxy FindElement(By by)
        {
            IWebElement element = null;
            try
            {
                OnNewInformation("Find " + by.ToString());
                element = _searchContext.FindElement(by);
                OnNewInformation($"Found Element { element?.Text ?? "(NULL)" }");
            }
            catch (NoSuchElementException)
            {
                CheckThrowNotFoundException(by);
            }

            var result = new WebElementProxy(element);
            return result;
        }

        internal IReadOnlyCollection<WebElementProxy> FindElements(By by)
        {
            IReadOnlyCollection<IWebElement> elements = null;
            try
            {
                OnNewInformation("Find " + by.ToString());
                elements = _searchContext.FindElements(by);
                OnNewInformation("Found Elements");
            }
            catch (NoSuchElementException)
            {
                CheckThrowNotFoundException(by);
            }

            var result = elements.Select(f => new WebElementProxy(f)).ToList();
            return result;
        }

        private void CheckThrowNotFoundException(By by)
        {
            if (ThrowExceptionWhenElementNotFound)
            {
                var exceptionMessage = $"Element not found. Selector was: { by }";
                throw new Infrastructure.Exceptions.WebElementNotFoundException(exceptionMessage);
            }
            else
            {
                Debug.WriteLine("Warning: Element By {0} not found!", by.ToString());
            }
        }

        private void OnNewInformation(string informationText)
        {
            var e = InformationPublished;
            if (e != null)
            {
                var loggingInfo = new LoggingInformation(informationText);
                var args = new Infrastructure.Delegates.InformationEventArgs(loggingInfo);
                e(this, args);
            }
        }
    }
}
