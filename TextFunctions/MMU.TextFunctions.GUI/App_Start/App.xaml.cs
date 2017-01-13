using System.Windows;
using Microsoft.Practices.Unity;
using MMU.TextFunctions.GUI.Views.Shell;

namespace MMU.TextFunctions.GUI.App_Start
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var unityContainer = new UnityContainer();
            Container.UnityInstance.SetUp(unityContainer);

            var viewContainer = unityContainer.Resolve<ViewContainer>();
            viewContainer.ShowDialog();
        }
    }
}