using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Intellivoid.Netlenium
{
    /// <summary>
    /// Netlenium Client
    /// </summary>
    public class Client
    {
        internal string session_id;
        internal string endpoint = "http://localhost:6410";
        internal string authentication_password;
        internal DriverType target_driver = DriverType.auto;
        internal Proxy proxy_configuration;

        /// <summary>
        /// The session ID that this client has created
        /// </summary>
        public string SessionID => session_id;

        /// <summary>
        /// Netlenium Server Endpoint
        /// </summary>
        public string Endpoint => endpoint;

        /// <summary>
        /// Authentication Password if required
        /// </summary>
        public string AuthenticationPassword => authentication_password;

        /// <summary>
        /// The target browser to use for this session
        /// </summary>
        public DriverType TargetDriver => target_driver;

        /// <summary>
        /// The proxy configuration used for this session
        /// </summary>
        public Proxy ProxyConfiguration => proxy_configuration;

        /// <summary>
        /// Returns the current window that's currently selected
        /// </summary>
        public Window CurrentWindow
        {
            get
            {
                try
                {
                    
                    var response = JObject.Parse(SendCommand("window_handler/current_window", true));
                    return new Window
                    {
                        Id = (string)response["CurrentWindow"]["ID"],
                        Url = (string)response["CurrentWindow"]["Url"],
                        Title = (string)response["CurrentWindow"]["Title"]
                    };

                }
                catch (RequestException requestException)
                {
                    ParseError(requestException.ResultsContent);
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the server details
        /// </summary>
        public Server Server
        {
            get
            {
                try
                {

                    var response = JObject.Parse(SendCommand(string.Empty));
                    DriverType defaultDriver;

                    switch((string)response["ServerDetails"]["DefaultDriver"])
                    {
                        case "chrome":
                            defaultDriver = DriverType.chrome;
                            break;

                        case "opera":
                            defaultDriver = DriverType.opera;
                            break;

                        case "firefox":
                            defaultDriver = DriverType.firefox;
                            break;

                        default:
                            defaultDriver = DriverType.auto;
                            break;
                    }

                    return new Server
                    {
                        Name = (string)response["ServerDetails"]["ServerName"],
                        Version = (string)response["ServerDetails"]["ServerVersion"],
                        ChromeDriversDisabled = (bool)response["ServerDetails"]["ChromeDriversDisabled"],
                        OperaDriversDisabled = (bool)response["ServerDetails"]["OperaDriversDisabled"],
                        FirefoxDriversDisabled = (bool)response["ServerDetails"]["FirefoxDriversDisabled"],
                        DefaultDriver = defaultDriver,
                        CurrentSessions = (int)response["ServerDetails"]["CurrentSessions"],
                        MaximumSessions = (int)response["ServerDetails"]["MaximumSessions"],
                        SessionInactivityLimit = (int)response["ServerDetails"]["SessionInactivityLimit"]
                    };

                }
                catch (RequestException requestException)
                {
                    ParseError(requestException.ResultsContent);
                }

                return null;
            }
        }
       
        /// <summary>
        /// Public Constructor with the Default Driver
        /// </summary>
        public Client()
        {
        }

        /// <summary>
        /// Public Constructor with a specified driver
        /// </summary>
        /// <param name="targetDriver"></param>
        public Client(DriverType targetDriver) => target_driver = targetDriver;

        /// <summary>
        /// Public Constructor with a specified driver and authentication password
        /// </summary>
        /// <param name="targetBrowser"></param>
        /// <param name="authenticationPassword"></param>
        public Client(DriverType targetDriver, string authenticationPassword)
        {
            authentication_password = authenticationPassword;
            target_driver = targetDriver;
        }

        /// <summary>
        /// Public constructor with a specified driver and proxy configuration
        /// </summary>
        /// <param name="targetDriver"></param>
        /// <param name="proxyConfiguration"></param>
        public Client(DriverType targetDriver, Proxy proxyConfiguration)
        {
            target_driver = targetDriver;
            proxy_configuration = proxyConfiguration;
        }

        /// <summary>
        /// Public constructor with a specified driver, proxy configuration and authentication password
        /// </summary>
        /// <param name="targetDriver"></param>
        /// <param name="proxyConfiguration"></param>
        /// <param name="authenticationPassword"></param>
        public Client(DriverType targetDriver, Proxy proxyConfiguration, string authenticationPassword)
        {
            target_driver = targetDriver;
            proxy_configuration = proxyConfiguration;
            authentication_password = authenticationPassword;
        }

        /// <summary>
        /// Public constructor with a specified endpoint
        /// </summary>
        /// <param name="endpoint"></param>
        public Client(string endpoint)
        {
            this.endpoint = endpoint;
        }

        /// <summary>
        /// Public constructor with a specified endpoint and specified driver
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="targetDriver"></param>
        public Client(string endpoint, DriverType targetDriver)
        {
            this.endpoint = endpoint;
            target_driver = targetDriver;
        }

        /// <summary>
        /// Public constructor with a specified endpoint, specified driver and authentication password
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="targetDriver"></param>
        /// <param name="authenticationPassword"></param>
        public Client(string endpoint, DriverType targetDriver, string authenticationPassword)
        {
            this.endpoint = endpoint;
            target_driver = targetDriver;
            authentication_password = authenticationPassword;
        }

        /// <summary>
        /// Public constructor with a specified endpoint, specified driver and proxy configuration
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="targetDriver"></param>
        /// <param name="proxyConfiguration"></param>
        public Client(string endpoint, DriverType targetDriver, Proxy proxyConfiguration)
        {
            this.endpoint = endpoint;
            target_driver = targetDriver;
            proxy_configuration = proxyConfiguration;
        }

        /// <summary>
        /// Public constructor with a specified endpoint, specified driver, proxy configuration and authentication password
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="targetDriver"></param>
        /// <param name="proxyConfiguration"></param>
        /// <param name="authenticationPassword"></param>
        public Client(string endpoint, DriverType targetDriver, Proxy proxyConfiguration, string authenticationPassword)
        {
            this.endpoint = endpoint;
            target_driver = targetDriver;
            proxy_configuration = proxyConfiguration;
            authentication_password = authenticationPassword;
        }

        /// <summary>
        /// Sends command to the server
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal string SendCommand(string command, Dictionary<string, string> parameters, bool include_session = false)
        {
            if(AuthenticationPassword != null)
            {
                parameters.Add("auth", AuthenticationPassword);
            }

            if(include_session == true)
            {
                if (SessionID == null)
                {
                    throw new SessionNotRunningException("The session is not running");
                }

                parameters.Add("session_id", SessionID);
            }

            return WebClient.SendCommand(endpoint, command, parameters);
        }

        /// <summary>
        /// Sends a command to the server without parameters
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        internal string SendCommand(string command, bool include_session = false)
        {
            return SendCommand(command, new Dictionary<string, string>(), include_session);
        }

        /// <summary>
        /// Parses the error and throws the proper exception
        /// </summary>
        /// <param name="code"></param>
        internal static void ParseError(string input)
        {
            JObject parsedInput;

            try
            {
                parsedInput = JObject.Parse(input);
            }
            catch(Exception ex)
            {
                throw new Exception($"Cannot parse '{input}', {ex.Message}");
            }
            

            switch ((int)parsedInput["ErrorCode"])
            {
                case 1:
                    throw new Exception(input);

                case 100:
                    throw new Exception((string)parsedInput["Error"]);

                case 101:
                    throw new AttributeNotFoundException((string)parsedInput["Message"]);

                case 102:
                    throw new InvalidProxySchemeException((string)parsedInput["Message"]);

                case 103:
                    throw new UnsupportedDriverException((string)parsedInput["Message"]);

                case 104:
                    throw new UnsupportedRequestMethodException((string)parsedInput["Message"]);

                case 105:
                    throw new SessionExpiredException((string)parsedInput["Message"]);

                case 106:
                    throw new WindowHandlerNotFoundException((string)parsedInput["Message"]);

                case 107:
                    throw new SessionErrorException((string)parsedInput["Message"]);

                case 108:
                    throw new SessionNotFoundException((string)parsedInput["Message"]);

                case 109:
                    throw new UnauthorizedException((string)parsedInput["Message"]);

                case 110:
                    throw new TooManysessionsException((string)parsedInput["Message"]);

                case 111:
                    throw new ResourceNotFoundException((string)parsedInput["Message"]);

                case 112:
                    throw new JavascriptExecutionException((string)parsedInput["Message"]);

                case 113:
                    throw new MissingParameterException((string)parsedInput["Message"]);

                case 114:
                    throw new InvalidSearchValueException((string)parsedInput["Message"]);

                case 115:
                    throw new DriverDisabledException((string)parsedInput["Message"]);

                case 116:
                    throw new ElementNotFoundException((string)parsedInput["Message"]);
                
                default:
                    throw new Exception(input);

            }
        }

        /// <summary>
        /// Starts the session
        /// </summary>
        public void Start()
        {
            try
            {
                var payload = new Dictionary<string, string>();

                if (TargetDriver != DriverType.auto)
                { 
                    payload.Add("target_driver", target_driver.ToString().ToLower());
                }

                if(ProxyConfiguration != null)
                {
                    if(ProxyConfiguration.Enabled == true)
                    {
                        payload.Add("proxy_host", ProxyConfiguration.Host);
                        payload.Add("proxy_port", Convert.ToString(ProxyConfiguration.Port));
                        payload.Add("proxy_scheme", ProxyConfiguration.Scheme.ToString().ToLower());

                        if (ProxyConfiguration.AuthenticationRequired == true)
                        {
                            payload.Add("proxy_username", ProxyConfiguration.Username);
                            payload.Add("proxy_password", ProxyConfiguration.Password);
                        }
                    }
                }

                var response = JObject.Parse(SendCommand("sessions/create", payload));
                session_id = (string)response["SessionId"];

            }
            catch(RequestException requestException)
            {
                ParseError(requestException.ResultsContent);
            }
        }

        /// <summary>
        /// Stops the session
        /// </summary>
        public void Stop()
        {
            try
            {
                SendCommand("sessions/close", true);
                session_id = null;
            }
            catch (RequestException requestException)
            {
                ParseError(requestException.ResultsContent);
            }
        }
        
        /// <summary>
        /// Navigates to the given URL
        /// </summary>
        /// <param name="url"></param>
        public void LoadUrl(string url)
        {
            try
            {
                var payload = new Dictionary<string, string>
                {
                    { "url", url }
                };
                SendCommand("navigation/load_url", payload, true);
            }
            catch (RequestException requestException)
            {
                ParseError(requestException.ResultsContent);
            }
        }

        /// <summary>
        /// Goes back one page in the history.
        /// </summary>
        public void GoBack()
        {
            try
            {
                SendCommand("navigation/go_back", true);
            }
            catch (RequestException requestException)
            {
                ParseError(requestException.ResultsContent);
            }
        }

        /// <summary>
        /// Goes forward one page in the history.
        /// </summary>
        public void GoForward()
        {
            try
            {
                SendCommand("navigation/go_forward", true);
            }
            catch (RequestException requestException)
            {
                ParseError(requestException.ResultsContent);
            }
        }

        /// <summary>
        /// Reloads the current page.
        /// </summary>
        public void Reload()
        {
            try
            {
                SendCommand("navigation/reload", true);
            }
            catch (RequestException requestException)
            {
                ParseError(requestException.ResultsContent);
            }
        }

        /// <summary>
        /// Returns a list of Elements
        /// </summary>
        /// <param name="by"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IReadOnlyList<Element> GetElements(By by, string value)
        {
            try
            {
                var payload = new Dictionary<string, string>
                {
                    { "by", by.GetDescription() },
                    { "value", value }
                };

                var response = JObject.Parse(SendCommand("actions/get_elements", payload, true));
                var elements = (JArray)response["Elements"];

                var returnResults = new List<Element>();
                foreach (var jToken in elements)
                {
                    var elementItem = (JObject) jToken;
                    returnResults.Add(new Element(this, by, value,  elements.IndexOf(elementItem))
                    {
                        Enabled = (bool)elementItem["Enabled"],
                        IsSelected = (bool)elementItem["IsSelected"],
                        Location = new System.Drawing.Point
                        {
                            X = (int)elementItem["ElementLocation"]["X"],
                            Y = (int)elementItem["ElementLocation"]["Y"]
                        },
                        Size = new System.Drawing.Size
                        {
                            Width = (int)elementItem["ElementSize"]["Width"],
                            Height = (int)elementItem["ElementSize"]["Height"]
                        },
                        TagName = (string)elementItem["TagName"],
                        InnerText = (string)elementItem["InnerText"],
                        InnerHtml = (string)elementItem["InnerHtml"]
                    });
                }

                return returnResults;
            }
            catch (RequestException requestException)
            {
                ParseError(requestException.ResultsContent); 
            }

            return null;
        }

        /// <summary>
        /// Gets the first element returned
        /// </summary>
        /// <param name="by"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Element GetElement(By by, string value)
        {
            var results = GetElements(by, value);

            if(results.Count == 0)
            {
                throw new ElementNotFoundException();
            }

            return results[0];
        }

        /// <summary>
        /// Executes Javascript code in the current window, returns the results if the code
        /// has a return statement
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string ExecuteJavascript(string code)
        {
            try
            {
                var payload = new Dictionary<string, string>
                {
                    { "code", code }
                };

                var response = JObject.Parse(SendCommand("actions/execute_javascript", payload));
                return (string)response["Output"];

            }
            catch (RequestException requestException)
            {
                ParseError(requestException.ResultsContent);
            }

            return null;
        }

        /// <summary>
        /// Closes the current window/tab that the session is currently
        /// focused on and switches back to another active window/tab. 
        /// </summary>
        public void Close()
        {
            try
            {
                SendCommand("actions/close", true);
            }
            catch (RequestException requestException)
            {
                ParseError(requestException.ResultsContent);
            }
        }

        /// <summary>
        /// Returns a list of Window Handler ID's that are opened (Tabs, Windows or Popups)
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<string> GetWindowHandlers()
        {
            try
            {

                var response = JObject.Parse(SendCommand("window_handler/list_windows", true));
                var windows = (JArray)response["WindowHandles"];

                var returnResults = new List<string>();
                foreach (var windowId in windows)
                {
                    returnResults.Add((string)windowId);
                }

                return returnResults;
            }
            catch (RequestException requestException)
            {
                ParseError(requestException.ResultsContent);
            }

            return null;
        }

        /// <summary>
        /// Switches to the ID of the window, tab or popup.
        /// </summary>
        /// <param name="id"></param>
        public void SwitchToWindow(string id)
        {
            try
            {
                var payload = new Dictionary<string, string>
                {
                    { "id", id }
                };

                SendCommand("window_handler/switch_to", payload);
            }
            catch (RequestException requestException)
            {
                ParseError(requestException.ResultsContent);
            }
        }

    }
}
