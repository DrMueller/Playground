using System;
using System.Runtime.Serialization;

namespace MMU.SeleniumExtensions.Core.Infrastructure.Exceptions
{
    [Serializable]
    public class WebElementNotFoundException : Exception
    {
        public WebElementNotFoundException()
        { }

        public WebElementNotFoundException(string message)
            : base(message)
        { }

        public WebElementNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected WebElementNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        { }
    }
}


