using System.ComponentModel;


namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Signals a command to end.
        /// </summary>
        public static CommandEnd end => new CommandEnd();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct CommandEnd {}
}