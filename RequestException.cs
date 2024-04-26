using System;
using System.Net;
using System.Runtime.Serialization;

namespace Intellivoid.Netlenium
{
    [Serializable]
    public class RequestException : Exception
    {
        private string result;
        private HttpStatusCode statusCode;

        public string ResultsContent => result;

        public HttpStatusCode StatusCode => statusCode;

        public RequestException()
        {
        }

        public RequestException(string message) : base(message)
        {
        }

        public RequestException(string result, HttpStatusCode statusCode)
        {
            this.result = result;
            this.statusCode = statusCode;
        }

        public RequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}