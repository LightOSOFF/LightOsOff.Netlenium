using System;
using System.Runtime.Serialization;

namespace Intellivoid.Netlenium
{
    [Serializable]
    public class InvalidSearchValueException : Exception
    {
        public InvalidSearchValueException()
        {
        }

        public InvalidSearchValueException(string message) : base(message)
        {
        }

        public InvalidSearchValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidSearchValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}