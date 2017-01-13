using MMU.TextFunctions.GUI.Commands;
using MMU.TextFunctions.GUI.Model;
using MMU.TextFunctions.GUI.ViewModels.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMU.TextFunctions.GUI.ViewModels
{
    class CsvFunctionsViewModel : Shell.ViewModelBase
    {
        public override string DisplayName
        {
            get
            {
                return "CSV-Functions";
            }
        }

        public ViewModelCommandList ViewModelCommands
        {
            get
            {
                return new ViewModelCommandList()
                {
                    CountLines
                };
            }
        }

        public int RowCount { get; private set; }

        private ViewModelCommand CountLines
        {
            get
            {
                return new ViewModelCommand("Count", new ActionCommand(() =>
                {
                    HandledAction(() =>
                    {
                        RowCount = GetRowCount();
                    });
                }, () => !string.IsNullOrEmpty(DataText)), "Count Rows");
            }
        }

        private int GetRowCount()
        {
            if (!string.IsNullOrEmpty(DataText))
            {
                var split = DataText.Split(new [] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                return split.Count();
            }

            return 0;
        } 
    }
}
