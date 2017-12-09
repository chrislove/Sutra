using System;
using JetBrains.Annotations;

namespace Sutra {
    internal sealed class EmptyPipeException : SutraException {
        public EmptyPipeException( string message ) : base(message) {}

        [NotNull]
        public static EmptyPipeException For<TPipe>() => For(typeof(TPipe));
        
        [NotNull]
        public static EmptyPipeException For(Type type) => new EmptyPipeException($"Pipe<{type}> is empty.");
    }
}