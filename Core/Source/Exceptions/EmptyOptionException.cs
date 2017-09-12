using System;
using JetBrains.Annotations;

namespace SharpPipe {
    internal sealed class EmptyOptionException : Exception {
        private EmptyOptionException( string message ) : base(message) {}

        [NotNull]
        public static EmptyOptionException For<T>() => new EmptyOptionException($"Option<{typeof(T)}> is empty.");
    }
}