using JetBrains.Annotations;

namespace SharpPipe {
    internal static class PipeOutput {
        public static PipeOutput<T> New<T>([CanBeNull] T value, bool shouldReturn) => new PipeOutput<T>(value, shouldReturn);
    }

    internal struct PipeOutput<T> {
        [CanBeNull] internal readonly T Contents;
        
        /// <summary>
        /// Signals for the whole pipe to stop executing and return.
        /// </summary>
        internal readonly bool ShouldSkip;
        
        internal PipeOutput( [CanBeNull] T value, bool shouldSkip ) {
            Contents = value;
            ShouldSkip = shouldSkip;
        }
    }
}