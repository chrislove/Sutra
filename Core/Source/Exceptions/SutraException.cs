using System;

namespace Sutra {
    internal class SutraException : Exception {
        public SutraException( string message ) : base(message) {}
    }
}