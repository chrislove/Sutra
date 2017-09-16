using System;
using JetBrains.Annotations;

namespace SharpPipe {
    internal sealed class EmptySequenceException : SharpPipeException {
        public EmptySequenceException( string message ) : base(message) {}

        [NotNull]
        public static EmptySequenceException For<TPipe>() => For(typeof(TPipe));
        
        [NotNull]
        public static EmptySequenceException For(Type type) => new EmptySequenceException($"Seq<{type}> is empty.");
    }
}