namespace SharpPipe {
    internal class PipeCommandException : SharpPipeException {
        public PipeCommandException( string commandName ) : base($"{commandName} command threw an exception.") { }
    }
}