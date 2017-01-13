using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMU.TextFunctions.GUI.Commands;
using MMU.TextFunctions.GUI.ViewModels.Shell;

namespace MMU.TextFunctions.GUI.ViewModels
{
    class JsonFunctionsViewModel : Shell.ViewModelBase
    {
        public override string DisplayName { get; } = "JSON-Stuff";

        public Model.ViewModelCommandList ViewModelCommands
        {
            get
            {
                return new Model.ViewModelCommandList()
                {
                    FormatJsonCommand
                };
            }
        }

        private ViewModelCommand FormatJsonCommand
        {
            get
            {
                return new ViewModelCommand("Format",
                    new ActionCommand(() =>
                    {
                        var ser = Newtonsoft.Json.JsonConvert.DeserializeObject(DataText);
                        var formatted = Newtonsoft.Json.JsonConvert.SerializeObject(ser, Newtonsoft.Json.Formatting.Indented);
                        DataText = formatted;
                    }));
            }
        }
    }



}
