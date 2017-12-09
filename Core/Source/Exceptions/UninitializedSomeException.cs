using System;
using JetBrains.Annotations;

namespace Sutra {
    internal sealed class UninitializedSomeException : Exception {
        internal UninitializedSomeException( [CanBeNull] string message ) : base(message) {}
    }
}