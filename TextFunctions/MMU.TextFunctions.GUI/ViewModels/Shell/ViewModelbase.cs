using MMU.TextFunctions.GUI.Commands;
using PropertyChanged;
using System;
using System.Windows;
using System.Windows.Input;

namespace MMU.TextFunctions.GUI.ViewModels.Shell
{
    [ImplementPropertyChanged]
    public abstract class ViewModelBase
    {
        public abstract string DisplayName { get; }

        public string DataText { get; set; }

        internal void RegisterRequestChangeInformationText(Action<string> action)
        {
            RequestChangeInformationText = action;
        }

        protected Action<string> RequestChangeInformationText;

        protected void HandledAction(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                RequestChangeInformationText(ex.Message);
            }
        }
    }
}
