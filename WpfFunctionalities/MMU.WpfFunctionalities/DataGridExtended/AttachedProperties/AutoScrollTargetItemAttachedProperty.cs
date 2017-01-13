using System.Windows;

namespace MMU.WpfFunctionalities.ControlExtensions.DataGrid.AttachedProperties
{
    // http://stackoverflow.com/questions/18019425/scrollintoview-for-wpf-datagrid-mvvm
    public class AutoScrollTargetItemAttachedProperty
    {
        public static readonly DependencyProperty AutoScrollTargetItemProperty =
            DependencyProperty.RegisterAttached("AutoScrollTargetItem",
                typeof(object),
                typeof(AutoScrollTargetItemAttachedProperty),
                new PropertyMetadata(AutoScrollTargetItemPropertyCanged));

        #region Public/Internal Methods

        public static object GetAutoScrollTargetItem(DependencyObject target)
        {
            return target.GetValue(AutoScrollTargetItemProperty);
        }

        public static void SetAutoScrollTargetItem(DependencyObject target, object value)
        {
            target.SetValue(AutoScrollTargetItemProperty, value);
        }

        #endregion

        #region Private Methods

        private static void AutoScrollTargetItemPropertyCanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as System.Windows.Controls.DataGrid;
            var newSelectedItem = e.NewValue;

            dataGrid.Dispatcher.InvokeAsync(() =>
            {
                dataGrid.UpdateLayout();
                dataGrid.ScrollIntoView(newSelectedItem, null);
            });
        }

        #endregion
    }
}