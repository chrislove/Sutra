using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Pipe conversion commands
        /// </summary>
        public static class to {
            /// <summary>
            /// Converts pipe to sequence.
            /// </summary>
            [PublicAPI]
            public static DoTransformToSeq<TIn, TOut> seq<TIn, TOut>(Func<TIn, IEnumerable<TOut>> func) => new DoTransformToSeq<TIn, TOut>(func);
        }
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoTransformToSeq<TIn, TOut> {
        private readonly ToSeqFunc<TIn, TOut> _func;

        internal DoTransformToSeq( [NotNull] Func<TIn, IEnumerable<TOut>> func ) => _func = func ?? throw new ArgumentNullException(nameof(func));

        public static Seq<TOut> operator |( Pipe<TIn> pipe, DoTransformToSeq<TIn, TOut> toSeq )
            => start<TOut>.seq | toSeq._func[pipe.Option];
    }
}