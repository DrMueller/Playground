using MMU.WpfFunctionalities.TestWpfUI.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for DataGridExtendedWindow.xaml
    /// </summary>
    public partial class DataGridExtendedWindow : Window
    {
        private readonly IEnumerable<Person> _persons;

        public IEnumerable<Person> Persons
        {
            get
            {
                return _persons;
            }
        }

        public DataGridExtendedWindow()
        {
            InitializeComponent();
            _persons = Factories.PersonFactory.CreatePersons();
            DataContext = this;
        }
    }
}
