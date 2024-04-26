using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intellivoid.Netlenium
{
    [Serializable]
    public class Server
    {
        /// <summary>
        /// The name of the server
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The version of the server
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Indicates if the chrome drivers has been disabled by the administrator
        /// </summary>
        public bool ChromeDriversDisabled { get; set; }

        /// <summary>
        /// Indicates if the opera drivers has been disabled by the administrator
        /// </summary>
        public bool OperaDriversDisabled { get; set; }

        /// <summary>
        /// Indicates if the firefox drivers has been disabled by the administrator
        /// </summary>
        public bool FirefoxDriversDisabled { get; set; }

        /// <summary>
        /// The default driver that gets used when no target browser is given
        /// </summary>
        public DriverType DefaultDriver { get; set; }

        /// <summary>
        /// The current number of sessions that are currently active
        /// </summary>
        public int CurrentSessions { get; set; }

        /// <summary>
        /// The maximum number of sessions that are allowed to be created
        /// </summary>
        public int MaximumSessions { get; set; }

        /// <summary>
        /// The maximum number of minutes a session is allowed to be inactive for
        /// </summary>
        public int SessionInactivityLimit { get; set; }
    }
}
