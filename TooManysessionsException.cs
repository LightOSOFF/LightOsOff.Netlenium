using System;
using System.Runtime.Serialization;

namespace Intellivoid.Netlenium
{
    [Serializable]
    public class TooManysessionsException : Exception
    {
        public TooManysessionsException()
        {
        }

        public TooManysessionsException(string message) : base(message)
        {
        }

        public TooManysessionsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TooManysessionsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}