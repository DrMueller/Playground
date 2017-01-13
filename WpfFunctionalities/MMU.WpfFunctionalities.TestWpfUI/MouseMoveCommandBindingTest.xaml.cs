using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MMU.WpfFunctionalities.TestWpfUI
{
    /// <summary>
    /// Interaction logic for MouseMoveCommandBindingTest.xaml
    /// </summary>
    public partial class MouseMoveCommandBindingTest : Window
    {
        public MouseMoveCommandBindingTest()
        {
            InitializeComponent();
        }
        
        public ICommand MoveCommand
        {
            get
            {
                return new TestCommand();
            }
        }

        public string CommandParameter
        {
            get
            {
                return "Test";
            }
        }
    }

    public class TestCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Debug.Write("Executed: " + parameter.ToString());
        }
    }
}
