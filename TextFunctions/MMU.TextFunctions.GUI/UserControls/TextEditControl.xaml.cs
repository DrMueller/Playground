using MMU.TextFunctions.GUI.ViewModels.Shell;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MMU.TextFunctions.GUI.UserControls
{
    /// <summary>
    /// Interaction logic for TextEditControl.xaml
    /// </summary>
    public partial class TextEditControl : UserControl
    {
        public static DependencyProperty ViewModelCommandsProperty = DependencyProperty.Register("ViewModelCommands", typeof(IEnumerable<ViewModels.Shell.ViewModelCommand>), typeof(TextEditControl));

        public IEnumerable<ViewModelCommand> ViewModelCommands
        {
            get
            {
                return (IEnumerable<ViewModelCommand>)GetValue(ViewModelCommandsProperty);
            }
            set
            {
                SetValue(ViewModelCommandsProperty, this);
            }
        }

        public TextEditControl()
        {
            InitializeComponent();
        }
    }
}
