using System;
using JetBrains.Annotations;

namespace Sutra {
    internal sealed class EmptySequenceException : SutraException {
        public EmptySequenceException( string message ) : base(message) {}

        [NotNull]
        public static EmptySequenceException For<TPipe>() => For(typeof(TPipe));
        
        [NotNull]
        public static EmptySequenceException For(Type type) => new EmptySequenceException($"Seq<{type}> is empty.");
    }
}