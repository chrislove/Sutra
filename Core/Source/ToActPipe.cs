namespace SharpPipe {
    public static partial class Pipe {
        /// <summary>
        /// Signals a pipe to convert to ActPipe.
        /// </summary>
        public static ToActPipe A => new ToActPipe();
    }
    
    public struct ToActPipe {}
}