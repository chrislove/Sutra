using JetBrains.Annotations;

namespace SharpPipe {
    internal sealed class EmptyPipeException : SharpPipeException {
        private EmptyPipeException( string message ) : base(message) {}
        
        [NotNull]
        public static EmptyPipeException For<TPipe>()
            => new EmptyPipeException($"The pipe of type {typeof(TPipe)} is an empty collection.");
    }
}