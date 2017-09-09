using JetBrains.Annotations;

namespace SharpPipe {
    internal class EmptyPipeException : SharpPipeException {
        public EmptyPipeException( string message ) : base(message) {}
        
        [NotNull]
        public static EmptyPipeException For<TPipe>()
            => new EmptyPipeException($"The pipe of type {typeof(TPipe)} is an empty collection.");
    }
}