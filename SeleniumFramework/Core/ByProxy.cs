using OpenQA.Selenium;

namespace MMU.SeleniumExtensions.Core
{
    public class ByProxy
    {
        internal By By { get; }

        public ByProxy(By by)
        {
            By = by;
        }

        public static ByProxy ClassName(string classNameToFind)
        {
            return new ByProxy(By.ClassName(classNameToFind));
        }
        
        public static ByProxy CssSelector(string cssSelectorToFind)
        {
            return new ByProxy(By.CssSelector(cssSelectorToFind));
        }

        public static ByProxy Id(string idToFind)
        {
            return new ByProxy(By.Id(idToFind));
        }

        public static ByProxy LinkText(string linkTextToFind)
        {
            return new ByProxy(By.LinkText(linkTextToFind));
        }

        public static ByProxy Name(string nameToFind)
        {
            return new ByProxy(By.Name(nameToFind));
        }

        public static ByProxy TagName(string tagNameToFind)
        {
            return new ByProxy(By.TagName(tagNameToFind));
        }

        public static ByProxy XPath(string xpathToFind)
        {
            return new ByProxy(By.XPath(xpathToFind));
        }
    }
}
