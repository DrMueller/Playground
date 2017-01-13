using System;
using System.Windows.Input;

namespace MMU.TextFunctions.GUI.Commands
{
    internal class ActionCommand : ICommand
    {
        private Func<bool> _canExecute;
        private Action _action;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public ActionCommand(Action action)
            : this(action, null)
        {
        }

        public ActionCommand(Action action, Func<bool> canExecute)
        {
            _canExecute = canExecute;
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
