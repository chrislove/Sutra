using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Appends a sequence of values to EnumPipe{T}.
        /// Usage: pipe | APPEND | 0 | 1 | 2 | I
        /// </summary>
        public static DoAppend APPEND => new DoAppend();
    }
    
    public struct DoAppend { }
    
    public partial struct DoStartPipe<T> {
        /// <summary>
        /// Starts a enumerable pipe
        /// </summary>
        public static DoAppend<T> operator |( DoStartPipe<T> pipe, DoAppend doAdd ) => new DoAppend<T>( EnumPipe<T>.Empty );
    }

    public partial struct EnumPipe<T> {
        public static DoAppend<T> operator |( EnumPipe<T> pipe, DoAppend DoAppend ) => new DoAppend<T>(pipe);
    }

    public struct DoAppend<T> {
        private readonly EnumPipe<T> _pipe;

        internal DoAppend( EnumPipe<T> pipe ) => _pipe = pipe;

        /// <summary>
        /// Pipe forward operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe{T}
        /// </summary>
        public static DoAppend<T> operator |(DoAppend<T> doAppend, [NotNull] IEnumerable<T> rhs) {
            if (rhs == null) throw new ArgumentNullException(nameof(rhs));

            return new DoAppend<T>(doAppend._pipe | ADD | rhs);
        }
		
        /// <summary>
        /// Pipe forward operator, adds a new value to IEnumerable{T}.
        /// </summary>
        public static DoAppend<T> operator |(DoAppend<T> doAppend, [CanBeNull] T rhs) => doAppend | Yield(rhs);


        /// <summary>
        /// Pipe forward operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe{T}
        /// </summary>
        public static DoAppend<T> operator |( DoAppend<T> doAppend, EnumPipe<T> rhs ) => doAppend | rhs.Get;


        public static EnumPipe<T> operator |( DoAppend<T> doAppend, CommandEnd end )
            => doAppend._pipe;

        
        private static IEnumerable<T> Yield([CanBeNull] T item) {
            yield return item;
        }
    }
}