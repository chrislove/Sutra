using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static class ENUM {
        /// <summary>
        /// Converts Pipe{T} to EnumPipe{T} using a converter function.
        /// </summary>
        public static DoConvertToEnum TO => new DoConvertToEnum();
    }
    
    public struct DoConvertToEnum { }

    public partial struct Pipe<T> {
        public static DoConvertToEnum<T> operator |( Pipe<T> pipe, DoConvertToEnum doConvertToEnum )
            => new DoConvertToEnum<T>( pipe );
    }
    

    public struct DoConvertToEnum<T> {
        private readonly Pipe<T> _pipe;

        internal DoConvertToEnum( Pipe<T> lhs ) => _pipe = lhs;

        /// <summary>
        /// Attaches a EnumPipe converter function to DoToPipe{T}.
        /// </summary>
        public static EnumPipe<T> operator |( DoConvertToEnum<T> lhs, [NotNull] Func<T, IEnumerable<T>> func )
                                => NEW<T>.PIPE
                                   | ADD | func(lhs._pipe.Get);

    }
}