using System.ComponentModel;

namespace Sutra {
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