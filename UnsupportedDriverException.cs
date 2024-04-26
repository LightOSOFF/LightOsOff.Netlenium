using System;
using System.Runtime.Serialization;

namespace Intellivoid.Netlenium
{
    [Serializable]
    public class UnsupportedDriverException : Exception
    {
        public UnsupportedDriverException()
        {
        }

        public UnsupportedDriverException(string message) : base(message)
        {
        }

        public UnsupportedDriverException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsupportedDriverException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}