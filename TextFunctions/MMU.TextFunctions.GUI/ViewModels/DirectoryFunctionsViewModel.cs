using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMU.TextFunctions.GUI.Commands;
using MMU.TextFunctions.GUI.Model;
using MMU.TextFunctions.GUI.ViewModels.Shell;

namespace MMU.TextFunctions.GUI.ViewModels
{
    public class DirectoryFunctionsViewModel : Shell.ViewModelBase
    {
        public override string DisplayName { get; } = "Directory-Functions";

        public IEnumerable<ViewModelCommand> ViewModelCommands
        {
            get
            {
                return new ViewModelCommandList
                {
                    ReadAllFilesFromDirectory
                };
            }
        }

        private ViewModelCommand ReadAllFilesFromDirectory
        {
            get
            {
                return new ViewModelCommand("Read from Dir", new ActionCommand(() =>
                    {
                        var sb = new StringBuilder();
                        var dirInfo = new DirectoryInfo(DataText);
                        var i = 1;
                        foreach(var file in dirInfo.GetFiles())
                        {
                            var str = "allXmlFiles({0}) := '{1}';";
                            str = string.Format(str, i++, file.Name);
                            sb.AppendLine(str);
                        }

                        DataText = sb.ToString();
                    }));
            }
        }
    }
}
