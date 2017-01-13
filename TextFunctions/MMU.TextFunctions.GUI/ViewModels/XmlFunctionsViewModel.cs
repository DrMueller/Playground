using System;
using MMU.TextFunctions.GUI.Commands;
using MMU.TextFunctions.GUI.ViewModels.Shell;
using PropertyChanged;
using System.IO;
using System.Xml;
using System.Text;

namespace MMU.TextFunctions.GUI.ViewModels
{
    [ImplementPropertyChanged]
    class XmlFunctionsViewModel : ViewModelBase
    {
        public override string DisplayName
        {
            get
            {
                return "XML-Functions";
            }
        }

        public Model.ViewModelCommandList ViewModelCommands
        {
            get
            {
                return new Model.ViewModelCommandList()
                {
                    FormatXmlCommand
                };
            }
        }

        private ViewModelCommand FormatXmlCommand
        {
            get
            {
                return new ViewModelCommand("Format", new ActionCommand(() =>
                {
                    HandledAction(FormatXml);
                }, () => !string.IsNullOrEmpty(DataText)));
            }
        }


        private void FormatXml()
        {
            using (var ms = new MemoryStream())
            using (var writer = new XmlTextWriter(ms, Encoding.Unicode))
            {
                var doc = new XmlDocument();
                doc.LoadXml(DataText);
                writer.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                doc.WriteContentTo(writer);
                writer.Flush();
                ms.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                ms.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                using (var sr = new StreamReader(ms))
                {
                    // Extract the text from the StreamReader.
                    var FormattedXML = sr.ReadToEnd();
                    DataText = FormattedXML;
                }
            }
        }
    }
}
