using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MMU.WpfFunctionalities.UserControls
{
    /// <summary>
    /// Interaction logic for DataGridExtended.xaml
    /// </summary>
    public partial class DataGridExtended : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty =
             DependencyProperty.Register("ItemsSource", typeof(IEnumerable),
             typeof(DataGridExtended));

        public static readonly DependencyProperty CanUserAddRowsProperty =
             DependencyProperty.Register("CanUserAddRows", typeof(bool),
             typeof(DataGridExtended));

        public static readonly DependencyProperty CanUserDeleteRowsProperty =
             DependencyProperty.Register("CanUserDeleteRows", typeof(bool),
             typeof(DataGridExtended));

        public static readonly DependencyProperty IsReadOnlyProperty =
             DependencyProperty.Register("IsReadOnly", typeof(bool),
             typeof(DataGridExtended));

        public bool IsReadOnly
        {
            get
            {
                return (bool)GetValue(IsReadOnlyProperty);
            }
            set
            {
                SetValue(IsReadOnlyProperty, value);
            }
        }

        public bool CanUserDeleteRows
        {
            get
            {
                return (bool)GetValue(CanUserDeleteRowsProperty);
            }
            set
            {
                SetValue(CanUserDeleteRowsProperty, value);
            }
        }

        public bool CanUserAddRows
        {
            get
            {
                return (bool)GetValue(CanUserAddRowsProperty);
            }
            set
            {
                SetValue(CanUserAddRowsProperty, value);
            }
        }



        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public DataGridExtended()
        {
            InitializeComponent();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var obj = (DependencyObject)e.OriginalSource;
            while (!(obj is DataGridRow) && obj != null)
            {
                obj = VisualTreeHelper.GetParent(obj);
            }

            if (obj is DataGridRow)
            {
                if ((obj as DataGridRow).DetailsVisibility == Visibility.Visible)
                {
                    (obj as DataGridRow).IsSelected = false;
                }
                else
                {
                    (obj as DataGridRow).IsSelected = true;
                }
            }
        }

        private void DataGrid_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            DataGridRow row = e.Row as DataGridRow;
            FrameworkElement tb = GetTemplateChildByName(row, "RowHeaderToggleButton");
            if (tb != null)
            {
                if (row.DetailsVisibility == Visibility.Visible)
                {
                    (tb as ToggleButton).IsChecked = true;
                }
                else
                {
                    (tb as ToggleButton).IsChecked = false;
                }
            }
        }

        private static FrameworkElement GetTemplateChildByName(DependencyObject parent, string name)
        {
            int childnum = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childnum; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is FrameworkElement && ((FrameworkElement)child).Name == name)
                {
                    return child as FrameworkElement;
                }
                else
                {
                    var frameWorkElement = GetTemplateChildByName(child, name);
                    if (frameWorkElement != null)
                    {
                        return frameWorkElement;
                    }
                }
            }

            return null;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var border = VisualTreeHelper.GetChild(dataGrid, 0) as Border;
            var scrollViewer = VisualTreeHelper.GetChild(border, 0) as ScrollViewer;
            var grid = VisualTreeHelper.GetChild(scrollViewer, 0) as Grid;
            var button = VisualTreeHelper.GetChild(grid, 0) as Button;

            if (button != null && button.Command != null && button.Command == DataGrid.SelectAllCommand)
            {
                button.IsEnabled = false;
                button.Opacity = 0;
            }
        }
    }
}
