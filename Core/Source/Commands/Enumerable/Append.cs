using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Appends a sequence of values to EnumerablePipe.
        /// Usage: pipe | APPEND | 0 | 1 | 2 | I
        /// </summary>
        public static DoAppend APPEND => new DoAppend();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAppend { }
    
    public partial struct DoToPipe<T> {
        /// <summary>
        /// Starts a enumerable pipe
        /// </summary>
        public static DoAppend<T> operator |( DoToPipe<T> pipe, DoAppend doAdd ) => new DoAppend<T>( EnumerablePipe<T>.Empty );
    }

    public partial struct EnumerablePipe<T> {
        public static DoAppend<T> operator |( EnumerablePipe<T> pipe, DoAppend DoAppend ) => new DoAppend<T>(pipe);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAppend<T> {
        private readonly EnumerablePipe<T> _pipe;

        internal DoAppend( EnumerablePipe<T> pipe ) => _pipe = pipe;

        /// <summary>
        /// Pipe forward operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe
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
        /// Pipe forward operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe
        /// </summary>
        public static DoAppend<T> operator |( DoAppend<T> doAppend, EnumerablePipe<T> rhs ) => doAppend | rhs.Get;


        public static EnumerablePipe<T> operator |( DoAppend<T> doAppend, CommandEnd end )
            => doAppend._pipe;

        
        private static IEnumerable<T> Yield([CanBeNull] T item) {
            yield return item;
        }
    }
}