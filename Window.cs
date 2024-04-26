namespace Intellivoid.Netlenium
{ 
    /// <summary>
    /// The active window details
    /// </summary>
    public class Window
    {
        /// <summary>
        /// The Window ID
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// The URL that this Window is currently on
        /// </summary>
        public string Url { get; internal set; }

        /// <summary>
        /// The title of the document / window
        /// </summary>
        public string Title { get; internal set; }
    }
}
