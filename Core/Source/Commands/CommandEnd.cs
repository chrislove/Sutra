namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Signals a command to end.
        /// </summary>
        public static CommandEnd I => new CommandEnd();
    }

    public struct CommandEnd {}
}