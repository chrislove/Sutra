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
            /// Converts Pipe to sequence
            /// </summary>
            [PublicAPI]
            public static DoTransformToSeq seq => new DoTransformToSeq();
        }
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoTransformToSeq { }

    public partial struct Pipe<T> {
        public static DoTransformToSeq<T> operator |( Pipe<T> pipe, DoTransformToSeq _ )
            => new DoTransformToSeq<T>( pipe );
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoTransformToSeq<T> {
        private readonly Pipe<T> _pipe;

        internal DoTransformToSeq( Pipe<T> pipe ) => _pipe = pipe;

        /// <summary>
        /// Transforms pipe to sequence using a function on the right.
        /// </summary>
        public static Seq<T> operator |( DoTransformToSeq<T> doTransform, [NotNull] Func<T, IEnumerable<T>> func ) {
            var pipeOut = doTransform._pipe.Value;
            
            foreach (var value in doTransform._pipe.Value) {
                return start<T>.seq | func(value);
            }

            return Seq<T>.SkipSeq;
        }
    }
}