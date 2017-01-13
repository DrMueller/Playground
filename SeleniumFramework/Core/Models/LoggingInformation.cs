namespace MMU.SeleniumExtensions.Core.Models
{
    public class LoggingInformation
    {
        public string InformationText { get; private set; }

        internal LoggingInformation(string informationText)
        {
            InformationText = informationText;
        }
    }
}
