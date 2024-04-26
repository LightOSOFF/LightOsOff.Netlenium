using System;
using System.Runtime.Serialization;

namespace Intellivoid.Netlenium
{
    /// <summary>
    /// The driver was disabled by the administrator
    /// </summary>
    [Serializable]
    public class DriverDisabledException : Exception
    {
        public DriverDisabledException()
        {
        }

        public DriverDisabledException(string message) : base(message)
        {
        }

        public DriverDisabledException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DriverDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}