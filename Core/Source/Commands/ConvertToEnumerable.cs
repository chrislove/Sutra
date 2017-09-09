using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Pipe conversion commands
        /// </summary>
        public static partial class TO {
            /// <summary>
            /// Converts Pipe to EnumerablePipe
            /// </summary>
            public static DoConvertToEnumerable ENUMERABLE => new DoConvertToEnumerable();
        }
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoConvertToEnumerable { }

    public partial struct Pipe<T> {
        public static DoConvertToEnumerable<T> operator |( Pipe<T> pipe, DoConvertToEnumerable doConvertToEnumerable )
            => new DoConvertToEnumerable<T>( pipe );
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoConvertToEnumerable<T> {
        private readonly Pipe<T> _pipe;

        internal DoConvertToEnumerable( Pipe<T> lhs ) => _pipe = lhs;

        /// <summary>
        /// Attaches a EnumerablePipe converter function to DoToPipe.
        /// </summary>
        public static EnumerablePipe<T> operator |( DoConvertToEnumerable<T> lhs, [NotNull] Func<T, IEnumerable<T>> func )
                                => START<T>.PIPE
                                   | ADD | func(lhs._pipe.Get);

    }
}