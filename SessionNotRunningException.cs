using System;
using System.Runtime.Serialization;

namespace Intellivoid.Netlenium
{
    [Serializable]
    public class SessionNotRunningException : Exception
    {
        public SessionNotRunningException()
        {
        }

        public SessionNotRunningException(string message) : base(message)
        {
        }

        public SessionNotRunningException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SessionNotRunningException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}