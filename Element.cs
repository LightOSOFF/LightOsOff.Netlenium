using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Intellivoid.Netlenium
{
    /// <summary>
    /// Element Object
    /// </summary>
    [Serializable]
    public class Element
    {
        private Client client;
        private By by;
        private string value;
        private int index;

        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="client"></param>
        /// <param name="by"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        public Element(Client client, By by, string value, int index)
        {
            this.client = client;
            this.by = by;
            this.value = value;
            this.index = index;
        }

        /// <summary>
        /// Indicates if this Element is enabled or not
        /// </summary>
        public bool Enabled { get; internal set; }

        /// <summary>
        /// Indicates if this Element is selected or not
        /// </summary>
        public bool IsSelected { get; internal set; }

        /// <summary>
        /// The current location of the element
        /// </summary>
        public Point Location { get; internal set; }

        /// <summary>
        /// The size of the element
        /// </summary>
        public Size Size { get; internal set; }

        /// <summary>
        /// The tag name used for this element
        /// </summary>
        public string TagName { get; internal set; }

        /// <summary>
        /// The text within this element
        /// </summary>
        public string InnerText { get; internal set; }

        /// <summary>
        /// The HTML contents within this element
        /// </summary>
        public string InnerHtml { get; internal set; }

        /// <summary>
        /// Sends a command to this Element
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        internal string SendCommand(string command, Dictionary<string, string> parameters)
        {
            try
            {
                parameters.Add("by", by.GetDescription());
                parameters.Add("value", value);
                parameters.Add("index", Convert.ToString(index));
                return client.SendCommand(string.Format($"web_element/{command}"), parameters, true);
            }
            catch (RequestException RequestException)
            {
                Client.ParseError(RequestException.ResultsContent);
            }

            return null;
        }

        /// <summary>
        /// Sends a command to this Element
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        internal string SendCommand(string command)
        {
            return SendCommand(command, new Dictionary<string, string>());
        }

        /// <summary>
        /// Clears the 
        /// </summary>
        public void Clear()
        {
            SendCommand("clear");
        }

        /// <summary>
        /// Simulates a click event
        /// </summary>
        public void Click()
        {
            SendCommand("click");
        }

        /// <summary>
        /// Moves into view of the element
        /// </summary>
        public void MoveTo()
        {
            SendCommand("move_to");
        }

        /// <summary>
        /// Submits the element to the remote server
        /// </summary>
        public void Submit()
        {
            SendCommand("submit");
        }

        /// <summary>
        /// Simulates typing into the element
        /// </summary>
        /// <param name="input"></param>
        public void SendKeys(string input)
        {
            var parameters = new Dictionary<string, string>
            {
                { "input", input }
            };
            SendCommand("send_keys", parameters);
        }

        /// <summary>
        /// Gets the value of a attribute
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetAttribute(string name)
        {
            var parameters = new Dictionary<string, string>
            {
                { "attribute_name", name }
            };

            var Response = JObject.Parse(SendCommand("get_attribute", parameters));
            return (string)Response["AttributeValue"];
        }

        /// <summary>
        /// Sets a value to a attribute / create the attribute with the given value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetAttribute(string name, string value)
        {
            var parameters = new Dictionary<string, string>
            {
                { "attribute_name", name },
                { "attribute_value", value}
            };

            SendCommand("set_attribute", parameters);
        }
    }
}
