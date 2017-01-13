using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MMU.WpfFunctionalities.TestWpfUI
{
    /// <summary>
    /// Interaction logic for SpinningBeachballWindow.xaml
    /// </summary>
    public partial class SpinningBeachballWindow : Window, INotifyPropertyChanged
    {
        public SpinningBeachballWindow()
        {
            InitializeComponent();
        }

        private bool _doIt;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool DoIt
        {
            get
            {
                return _doIt;
            }
            set
            {
                _doIt = value;
                OnPropertyChanged();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoIt = !DoIt;
        }

    }
}
