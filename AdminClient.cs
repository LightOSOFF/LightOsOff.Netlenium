using System.Collections.Generic;

namespace LightOsOff.Netlenium
{
    /// <summary>
    /// Netlenium Client
    /// </summary>
    public class AdminClient
    {
        private readonly string endpoint = "http://localhost:6410";
        private readonly string authenticationPassword;

        /// <summary>
        /// Netlenium Server Endpoint
        /// </summary>
        public string Endpoint => endpoint;

        /// <summary>
        /// Authentication Password if required
        /// </summary>
        private string AuthenticationPassword => authenticationPassword;

        /// <summary>
        /// Public Constructor with the Default Driver
        /// </summary>
        public AdminClient()
        {
        }

        /// <summary>
        /// Public Constructor with a specified authentication password
        /// </summary>
        /// <param name="authenticationPassword">The authentication password.</param>
        public AdminClient(string authenticationPassword)
        {
            this.authenticationPassword = authenticationPassword;
        }

        /// <summary>
        /// Sends command to the server
        /// </summary>
        /// <param name="command">The command to send.</param>
        /// <param name="parameters">The parameters of the command.</param>
        /// <returns>The response from the server.</returns>
        private string SendCommand(string command, Dictionary<string, string> parameters)
        {
            if (AuthenticationPassword != null)
            {
                parameters.Add("auth", AuthenticationPassword);
            }

            return WebClient.SendCommand(Endpoint, command, parameters);
        }

        /// <summary>
        /// Sends a command to the server without parameters
        /// </summary>
        /// <param name="command">The command to send.</param>
        /// <returns>The response from the server.</returns>
        private string SendCommand(string command)
        {
            return SendCommand(command, new Dictionary<string, string>());
        }

        /// <summary>
        /// Parses the error and throws the proper exception
        /// </summary>
        /// <param name="input">The input to parse.</param>
        private static void ParseError(string input)
        {
            try
            {
                var parsedInput = JObject.Parse(input);
                var errorCode = (int)parsedInput["ErrorCode"];
                var errorMessage = (string)parsedInput["Message"];

                switch (errorCode)
                {
                    case 1:
                        throw new Exception(input);

                    case 100:
                        throw new Exception(errorMessage);

                    case 101:
                        throw new AttributeNotFoundException(errorMessage);

                    case 102:
                        throw new InvalidProxySchemeException(errorMessage);

                    case 103:
                        throw new UnsupportedDriverException(errorMessage);

                    case 104:
                        throw new UnsupportedRequestMethodException(errorMessage);

                    case 105:
                        throw new SessionExpiredException(errorMessage);

                    case 106:
                        throw new WindowHandlerNotFoundException(errorMessage);

                    case 107:
                        throw new SessionErrorException(errorMessage);

                    case 108:
                        throw new SessionNotFoundException(errorMessage);

                    case 109:
                        throw new UnauthorizedException(errorMessage);

                    case 110:
                        throw new TooManysessionsException(errorMessage);

                    case 111:
                        throw new ResourceNotFoundException(errorMessage);

                    case 112:
                        throw new JavascriptExecutionException(errorMessage);

                    case 113:
                        throw new MissingParameterException(errorMessage);

                    case 114:
                        throw new InvalidSearchValueException(errorMessage);

                    case 115:
                        throw new DriverDisabledException(errorMessage);

                    case 116:
                        throw new ElementNotFoundException(errorMessage);

                    default:
                        throw new Exception($"Unknown error code: {errorCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot parse '{input}', {ex.Message}");
            }
        }

        /// <summary>
        /// Returns a list of active sessions
        /// </summary>
        /// <returns>The list of active sessions.</returns>
        public IReadOnlyList<Session> GetSessions()
        {
            try
            {
                var response = JObject.Parse(SendCommand("admin/active_sessions"));
                var sessions = (JArray)response["Sessions"];

                var returnResults = new List<Session>();
                foreach (var jToken in sessions)
                {
                    var sessionItem = (JObject)jToken;
                    var driver = GetDriverType((string)sessionItem["Driver"]);
                    var scheme = GetProxyScheme((string)sessionItem["ProxyConfiguration"]["Scheme"]);

                    var session = new Session
                    {
                        Id = (string)sessionItem["ID"],
                        Created = (string)sessionItem["Created"],
                        LastActivity = (string)sessionItem["LastActivity"],
                        Driver = driver,
                        CurrentWindow = new Window
                        {
                            Id = (string)sessionItem["CurrentWindow"]["ID"],
                            Title = (string)sessionItem["CurrentWindow"]["Title"],
                            Url = (string)sessionItem["CurrentWindow"]["Url"]
                        },
                        ProxyConfiguration = new Proxy
                        {
                            Enabled = (bool)sessionItem["ProxyConfiguration"]["Enabled"],
                            Scheme = scheme,
                            Host = (string)sessionItem["ProxyConfiguration"]["Host"],
                            Port = (int)sessionItem["ProxyConfiguration"]["Port"],
                            AuthenticationRequired = (bool)sessionItem["ProxyConfiguration"]["AuthenticationRequired"],
                            Username = (string)sessionItem["ProxyConfiguration"]["Username"],
                            Password = (string)sessionItem["ProxyConfiguration"]["Password"]
                        }
                    };

                    returnResults.Add(session);
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
        /// Converts a string representation of a driver to the corresponding DriverType enum value.
        /// </summary>
        /// <param name="driverString">The string representation of the driver.</param>
        /// <returns>The corresponding DriverType enum value.</returns>
        private static DriverType GetDriverType(string driverString)
        {
            return driverString switch
            {
                "chrome" => DriverType.chrome,
                "firefox" => DriverType.firefox,
                "opera" => DriverType.opera,
                _ => DriverType.auto
            };
        }

        /// <summary>
        /// Converts a string representation of a proxy scheme to the corresponding ProxyScheme enum value.
        /// </summary>
        /// <param name="schemeString">The string representation of the proxy scheme.</param>
        /// <returns>The corresponding ProxyScheme enum value.</returns>
        private static ProxyScheme GetProxyScheme(string schemeString)
        {
            return schemeString switch
            {
                "http" => ProxyScheme.http,
                "https" => ProxyScheme.https,
                _ => ProxyScheme.http
            };
        }
    }
}

/// <summary>
/// Sends a command to the server without parameters
/// </summary>
/// <param name="command"></param>
/// <returns></returns>
private string SendCommand(string command)
{
    return SendCommand(command, new Dictionary<string, string>());
}

/// <summary>
/// Parses the error and throws the proper exception
/// </summary>
/// <param name="input"></param>
private static void ParseError(string input)
{
    JObject parsedInput;

    try
    {
        parsedInput = JObject.Parse(input);
    }
    catch (Exception ex)
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
    }
}

/// <summary>
/// Returns a list of active sessions
/// </summary>
/// <returns></returns>
public IReadOnlyList<Session> GetSessions()
{
    try
    {
        var response = JObject.Parse(SendCommand("admin/active_sessions"));
        var sessions = (JArray)response["Sessions"];

        var returnResults = new List<Session>();
        foreach (var jToken in sessions)
        {
            var sessionItem = (JObject)jToken;
            DriverType driver;
            ProxyScheme scheme;

            switch ((string)sessionItem["Driver"])
            {
                case "chrome":
                    driver = DriverType.chrome;
                    break;

                case "firefox":
                    driver = DriverType.firefox;
                    break;

                case "opera":
                    driver = DriverType.opera;
                    break;

                default:
                    driver = DriverType.auto;
                    break;
            }

            switch ((string)sessionItem["ProxyConfiguration"]["Scheme"])
            {
                case "http":
                    scheme = ProxyScheme.http;
                    break;

                case "https:":
                    scheme = ProxyScheme.https;
                    break;

                default:
                    scheme = ProxyScheme.http;
                    break;
            }

            returnResults.Add(new Session
            {
                Id = (string)sessionItem["ID"],
                Created = (string)sessionItem["Created"],
                LastActivity = (string)sessionItem["LastActivity"],
                Driver = driver,
                CurrentWindow = new Window
                {
                    Id = (string)sessionItem["CurrentWindow"]["ID"],
                    Title = (string)sessionItem["CurrentWindow"]["Title"],
                    Url = (string)sessionItem["CurrentWindow"]["Url"]
                },
                ProxyConfiguration = new Proxy
                {
                    Enabled = (bool)sessionItem["ProxyConfiguration"]["Enabled"],
                    Scheme = scheme,
                    Host = (string)sessionItem["ProxyConfiguration"]["Host"],
                    Port = (int)sessionItem["ProxyConfiguration"]["Port"],
                    AuthenticationRequired = (bool)sessionItem["ProxyConfiguration"]["AuthenticationRequired"],
                    Username = (string)sessionItem["ProxyConfiguration"]["Username"],
                    Password = (string)sessionItem["ProxyConfiguration"]["Password"]
                }
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

    }
}
