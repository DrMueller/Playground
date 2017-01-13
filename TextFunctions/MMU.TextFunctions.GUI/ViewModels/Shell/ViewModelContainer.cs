using System;
using PropertyChanged;
using System.Collections.Generic;
using MMU.TextFunctions.GUI.Commands;

namespace MMU.TextFunctions.GUI.ViewModels.Shell
{
    [ImplementPropertyChanged]
    class ViewModelContainer
    {
        private readonly ViewModelFactory _viewModelFactory;

        public ViewModelContainer(ViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
            InformationText = "Here could be your Text..";
        }

        internal void HandledAction(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                InformationText = ex.Message;
            }
        }


        public IEnumerable<ViewModelCommand> NavigationCommands
        {
            get
            {
                return new List<ViewModelCommand>
                {
                    new ViewModelCommand("XML",
                        new ActionCommand(
                            ApplyViewModel<XmlFunctionsViewModel>,
                            () => true)),
                    new ViewModelCommand("List",
                        new ActionCommand(
                            ApplyViewModel<ListFunctionsViewModel>,
                            () => true)),
                    new ViewModelCommand("CSV",
                        new ActionCommand(
                            ApplyViewModel<CsvFunctionsViewModel>,
                            () => true)),
                    new ViewModelCommand("JSON",
                        new ActionCommand(
                            ApplyViewModel<JsonFunctionsViewModel>,
                            () => true)),
                    new ViewModelCommand("Directory",
                        new ActionCommand(
                            ApplyViewModel<DirectoryFunctionsViewModel>,
                            () => true))
                };
            }
        }

        private void ApplyViewModel<T>()
            where T : ViewModelBase
        {
            HandledAction(
                () =>
                {
                    var vm = _viewModelFactory.CreateViewModel<T>();
                    vm.RegisterRequestChangeInformationText(OnRequestChangeInformationText);
                    CurrentContent = vm;
                });
        }

        private void OnRequestChangeInformationText(string text)
        {
            InformationText = text;
        }

        public ViewModelBase CurrentContent { get; private set; }

        public string InformationText { get; private set; }
    }
}
