using System;

namespace Intellivoid.Netlenium
{
    /// <summary>
    /// Session Object
    /// </summary>
    [Serializable]
    public class Session
    {
        /// <summary>
        /// The session ID
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// The date that this session was created
        /// </summary>
        public string Created { get; internal set; }

        /// <summary>
        /// The date that this session was last used
        /// </summary>
        public string LastActivity { get; internal set; }

        /// <summary>
        /// The driver that the session is currently using
        /// </summary>
        public DriverType Driver { get; internal set; } 

        /// <summary>
        /// Indicates if this session is running in headless mode
        /// </summary>
        public bool Headless { get; internal set; }

        /// <summary>
        /// The proxy configuration that this session is using
        /// </summary>
        public Proxy ProxyConfiguration { get; internal set; }

        /// <summary>
        /// The current window that this session is focused on
        /// </summary>
        public Window CurrentWindow { get; internal set; }

        /// <summary>
        /// Creates a client for this session
        /// </summary>
        /// <param name="adminClient"></param>
        /// <returns></returns>
        public Client CreateClient(AdminClient adminClient)
        {
            return new Client()
            {
                endpoint = adminClient.Endpoint,
                session_id = Id,
                target_driver = Driver,
                proxy_configuration = ProxyConfiguration
            };
        }

        /// <summary>
        /// Creates a client for this session with a authentication password
        /// </summary>
        /// <param name="adminClient"></param>
        /// <param name="authenticationPassword"></param>
        /// <returns></returns>
        public Client CreateClient(AdminClient adminClient, string authenticationPassword)
        {
            var Client = CreateClient(adminClient);
            Client.authentication_password = authenticationPassword;
            return Client;
        }
    }
}
