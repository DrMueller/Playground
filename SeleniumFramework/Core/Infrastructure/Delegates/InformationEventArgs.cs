using MMU.SeleniumExtensions.Core.Models;
using System;

namespace MMU.SeleniumExtensions.Core.Infrastructure.Delegates
{
    public class InformationEventArgs : EventArgs
    {
        public InformationEventArgs(LoggingInformation loggingInformation)
        {
            LoggingInformation = loggingInformation;
        }

        public LoggingInformation LoggingInformation { get; private set; }
    }
}
