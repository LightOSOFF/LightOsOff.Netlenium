namespace Intellivoid.Netlenium
{
    /// <summary>
    /// Proxy Configuration for the session
    /// </summary>
    public class Proxy
    {
        /// <summary>
        /// If this proxy configuration is enabled or not
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// The proxy scheme that's being used
        /// </summary>
        public ProxyScheme Scheme { get; set; }

        /// <summary>
        /// Proxy host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Proxy Port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// If authentication is required for this proxy
        /// </summary>
        public bool AuthenticationRequired { get; set; }

        /// <summary>
        /// The username used for authentication
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password used for authentication
        /// </summary>
        public string Password { get; set; }
    }
}
