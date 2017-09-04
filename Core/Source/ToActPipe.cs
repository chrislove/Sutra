namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Signals a pipe to convert to ActPipe.
        /// </summary>
        public static ToActPipe A => new ToActPipe();
    }
    
    public struct ToActPipe {}

    public partial struct Pipe<TOut> {
        public static ActPipe<TOut> operator -( Pipe<TOut> lhs, ToActPipe rhs )        => ActPipe.FromPipe(lhs);
    }
}