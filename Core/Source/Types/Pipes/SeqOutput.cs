using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
    internal static class SeqOutput {
        public static SeqOutput<T> New<T>([CanBeNull] IEnumerable<T> value, bool shouldReturn) => new SeqOutput<T>(value, shouldReturn);
    }
    
    internal struct SeqOutput<T> {
        [CanBeNull] internal readonly IEnumerable<T> Contents;
        
        /// <summary>
        /// Signals for the whole pipe to stop executing and return.
        /// </summary>
        internal readonly bool ShouldSkip;
        
        internal SeqOutput( [CanBeNull] IEnumerable<T> value, bool shouldSkip ) {
            Contents   = value;
            ShouldSkip = shouldSkip;
        }
    }
}