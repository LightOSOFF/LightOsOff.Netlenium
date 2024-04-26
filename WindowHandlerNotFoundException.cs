using System;
using System.Runtime.Serialization;

namespace Intellivoid.Netlenium
{
    [Serializable]
    public class WindowHandlerNotFoundException : Exception
    {
        public WindowHandlerNotFoundException()
        {
        }

        public WindowHandlerNotFoundException(string message) : base(message)
        {
        }

        public WindowHandlerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WindowHandlerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}