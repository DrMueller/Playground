using Microsoft.Practices.Unity;
using System;
using System.Threading;

namespace MMU.SeleniumExtensions.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var uc = new UnityContainer();
            Core.Initializer.Initialize(uc);
            var driver = uc.Resolve<Core.RemoteWebDriverProxy>();

            driver.ThrowExceptionWhenElementNotFound = true;
            driver.Initialize(Core.Models.Enumerations.BrowserType.InternetExplorer);
            driver.InformationPublished += Driver_InformationPublished;

            driver.NavigateTo("https://boerse.to/");

            var signup = driver.FindElementById("SignupButton");
            signup.Click();

            Thread.Sleep(1000);

            var login = driver.FindElementById("LoginControl");
            login.SendKeys("EdwinVanDota");
            var pw = driver.FindElementById("ctrl_password");
            pw.SendKeys("joker1");

            var remember = driver.FindElementByName("remember");
            remember.Click();

            var loginSubmit = driver.FindElementByCssSelector("input[value=Anmelden]");

            loginSubmit.Click();

            Console.ReadKey();
        }

        private static void Driver_InformationPublished(object sender, Core.Infrastructure.Delegates.InformationEventArgs e)
        {
            Console.WriteLine(e.LoggingInformation.InformationText);
        }
    }
}
