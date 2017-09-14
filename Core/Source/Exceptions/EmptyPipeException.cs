using System;
using JetBrains.Annotations;

namespace SharpPipe {
    internal sealed class EmptyPipeException : SharpPipeException {
        public EmptyPipeException( string message ) : base(message) {}

        [NotNull]
        public static EmptyPipeException For<TPipe>() => For(typeof(TPipe));
        
        [NotNull]
        public static EmptyPipeException For(Type type) => new EmptyPipeException($"The pipe of type {type} is empty.");
    }
}