using MMU.TextFunctions.GUI.Commands;
using MMU.TextFunctions.GUI.Model;
using MMU.TextFunctions.GUI.ViewModels.Shell;
using PropertyChanged;
using System;
using System.Text;

namespace MMU.TextFunctions.GUI.ViewModels
{
    [ImplementPropertyChanged]
    class ListFunctionsViewModel : ViewModelBase
    {
        public override string DisplayName
        {
            get
            {
                return "List-Functions";
            }
        }

        public ViewModelCommandList ViewModelCommands
        {
            get
            {
                return new ViewModelCommandList
                {
                    ToCommaSeperatedListCommand,
                    ToCommaSeperatedStringListCommand,
                    ToSpaceSeparatedStringCommand,
                    ToCommaAndTabbedDoubleQuoteList
                };
            }
        }

        private ViewModelCommand ToCommaSeperatedListCommand
        {
            get
            {
                return new ViewModelCommand("T to S", new ActionCommand(() =>
                {
                    HandledAction(FormatToCommaSeperatedList);
                }, () => !string.IsNullOrEmpty(DataText)), "Formats a Tab-Seperated List to a Comma-seperated List");
            }
        }


        private ViewModelCommand ToSpaceSeparatedStringCommand
        {
            get
            {
                return new ViewModelCommand(", to Sp", new ActionCommand(() =>
                {
                    HandledListFormatAction(FormatToSpaceSeparatedString);
                }, ToSpaceSeparatedStringCommandCanExecute), "Formats a Comma-Seperated List to a Space-Seperated String");
            }
        }

        private ViewModelCommand ToCommaSeperatedStringListCommand
        {
            get
            {
                return new ViewModelCommand("T to \'S\'", new ActionCommand(() =>
                {
                    HandledListFormatAction(FormatToCommaSeperatedStringList);
                }, () => !string.IsNullOrEmpty(DataText)), "Formats a Tab-Seperated List to a Comma-seperated String-List");
            }
        }

        private ViewModelCommand ToCommaAndTabbedDoubleQuoteList
        {
            get
            {
                return new ViewModelCommand("T to Tab-\"s\"'", new ActionCommand(() =>
                                                                            {
                                                                                HandledListFormatAction(FormatToCommaAndTabbedDoubleQuoteList);
                                                                            }, () => !string.IsNullOrEmpty(DataText)), "Formats a Comma-Seperated List to a tabbed and double quoted comma-list");
            }
        }

        private void FormatToCommaAndTabbedDoubleQuoteList()
        {
            var splitEntries = DataText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();

            foreach (var splitEntry in splitEntries)
            {
                sb.Append(", \"");
                sb.Append(splitEntry);
                sb.AppendLine("\"");
            }

            sb.Remove(0, 2);
            DataText = sb.ToString();
        }

        private void HandledListFormatAction(Action action)
        {
            HandledAction(() =>
            {
                RemoveWhiteSpacesFromDataText();
                action();
            });
        }

        private void RemoveWhiteSpacesFromDataText()
        {
            DataText = DataText.Replace(" ", string.Empty);
        }

        private bool ToSpaceSeparatedStringCommandCanExecute()
        {
            if (string.IsNullOrEmpty(DataText))
            {
                return false;
            }
            
            if (!DataText.Contains(","))
            {
                return false;
            }

            return true;
        }

        private void FormatToSpaceSeparatedString()
        {
            var str = DataText.Replace(Environment.NewLine, " ");
            str = str.Replace(",", string.Empty);
            DataText = str;
        }

        private void FormatToCommaSeperatedStringList()
        {
            var splitEntries = DataText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();

            foreach (var splitEntry in splitEntries)
            {
                sb.Append(", '");
                sb.Append(splitEntry);
                sb.Append("'");
            }

            sb.Remove(0, 2);
            DataText = sb.ToString();
        }

        private void FormatToCommaSeperatedList()
        {
            var splitEntries = DataText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();

            foreach(var splitEntry in splitEntries)
            {
                sb.Append(", ");
                sb.Append(splitEntry);
            }

            sb.Remove(0, 2);
            DataText = sb.ToString();
        }
    }
}
