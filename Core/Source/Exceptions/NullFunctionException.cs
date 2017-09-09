using JetBrains.Annotations;

namespace SharpPipe {
    internal sealed class NullFunctionException : SharpPipeException {
        private NullFunctionException( string message ) : base(message) { }
        
        [NotNull]
        public static NullFunctionException For<TFunc>()
            => new NullFunctionException($"The function of type {typeof(TFunc)} is null.");
    }
}