using System.Collections.Generic;
using System.Net.Http;

namespace Intellivoid.Netlenium
{
    /// <summary>
    /// HTTP Web Client for communicating to a HTTP Server
    /// </summary>
    internal static class WebClient
    {
        /// <summary>
        /// Sends command to the server
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal static string SendCommand(string endpoint, string command, Dictionary<string, string> parameters)
        {

            using (var client = new HttpClient())
            {
                string RequestLocation;

                if (command == string.Empty)
                {
                    RequestLocation = endpoint;
                }
                else
                {
                    RequestLocation = string.Format("{0}/{1}", endpoint, command);
                }

                var response = client.PostAsync(RequestLocation, new FormUrlEncodedContent(parameters)).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    throw new RequestException(response.Content.ReadAsStringAsync().Result, response.StatusCode);
                }
            }
        }

        /// <summary>
        /// Sends a command to the server without paramerters
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        internal static string SendCommand(string endpoint, string command)
        {
            return SendCommand(endpoint, command, new Dictionary<string, string>());
        }
    }
}
