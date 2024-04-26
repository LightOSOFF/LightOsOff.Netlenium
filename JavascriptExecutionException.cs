using System;
using System.Runtime.Serialization;

namespace Intellivoid.Netlenium
{
    [Serializable]
    public class JavascriptExecutionException : Exception
    {
        public JavascriptExecutionException()
        {
        }

        public JavascriptExecutionException(string message) : base(message)
        {
        }

        public JavascriptExecutionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected JavascriptExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}