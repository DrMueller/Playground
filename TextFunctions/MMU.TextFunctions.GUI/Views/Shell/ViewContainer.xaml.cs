using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using MMU.TextFunctions.GUI.Commands;

namespace MMU.TextFunctions.GUI.Views.Shell
{
    /// <summary>
    /// Interaction logic for ViewContainer.xaml
    /// </summary>
    public partial class ViewContainer : Window
    {
        public ViewContainer()
        {
            DataContext = Container.UnityInstance.Instance.Resolve<ViewModels.Shell.ViewModelContainer>();
            InitializeComponent();
        }

        public ICommand CloseCommand => new ActionCommand(Close);
    }
}
