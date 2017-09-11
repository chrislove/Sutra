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
        public static partial class to {
            /// <summary>
            /// Converts Pipe to sequence
            /// </summary>
            public static DoConvertToSeq seq => new DoConvertToSeq();
        }
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoConvertToSeq { }

    public partial struct Pipe<T> {
        public static DoConvertToSeq<T> operator |( Pipe<T> pipe, DoConvertToSeq doConvertToSeq )
            => new DoConvertToSeq<T>( pipe );
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoConvertToSeq<T> {
        private readonly Pipe<T> _pipe;

        internal DoConvertToSeq( Pipe<T> pipe ) => _pipe = pipe;

        /// <summary>
        /// Attaches a sequence converter function to DoToPipe.
        /// </summary>
        public static Seq<T> operator |( DoConvertToSeq<T> doConvert, [NotNull] Func<T, IEnumerable<T>> func )
                                => start<T>.pipe
                                   | add | func(doConvert._pipe.Get);

    }
}