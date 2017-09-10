namespace SharpPipe {
    internal sealed class NullPipeException : SharpPipeException {
        public NullPipeException( string message ) : base(message) { }
    }
}