using System.Windows.Input;

namespace MMU.TextFunctions.GUI.ViewModels.Shell
{
    public class ViewModelCommand
    {
        #region Constructors

        public ViewModelCommand(string displayName, ICommand command) : this(displayName, command, string.Empty)
        {
        }

        public ViewModelCommand(string displayName, ICommand command, string toolTipText)
        {
            DisplayName = displayName;
            Command = command;
            ToolTipText = toolTipText;
        }

        #endregion Constructors

        #region Properties

        public ICommand Command { get; private set; }

        public string DisplayName { get; private set; }

        public string ToolTipText { get; private set; }
        #endregion Properties
    }
}
