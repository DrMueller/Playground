using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MMU.WpfFunctionalities.TestWpfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnDataGridExtended_Click(object sender, RoutedEventArgs e)
        {
            var w = new DataGridExtendedWindow();
            w.Show();
        }

        private void btnSpinningBeachBall_Click(object sender, RoutedEventArgs e)
        {
            var w = new SpinningBeachballWindow();
            w.Show();
        }
    }
}
