using System.ComponentModel;


namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Signals a command to end.
        /// </summary>
        public static CommandEnd end => new CommandEnd();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct CommandEnd {}
}