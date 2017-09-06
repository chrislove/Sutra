using System;

namespace SharpPipe {
    internal class SharpPipeException : Exception {
        public SharpPipeException( string message ) : base(message) {}
    }
}