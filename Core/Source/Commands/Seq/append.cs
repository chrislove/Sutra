using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;



namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Appends a sequence of values to EnumerablePipe.
        /// Usage: pipe | APPEND | 0 | 1 | 2 | I
        /// </summary>
        public static DoAppend append => new DoAppend();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAppend { }
    
    public partial struct DoToPipe<T> {
        /// <summary>
        /// Starts a enumerable pipe
        /// </summary>
        public static DoAppend<T> operator |( DoToPipe<T> pipe, DoAppend doAdd ) => new DoAppend<T>( Seq<T>.empty );
    }

    public partial struct Seq<T> {
        public static DoAppend<T> operator |( Seq<T> pipe, DoAppend DoAppend ) => new DoAppend<T>(pipe);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAppend<T> {
        private readonly Seq<T> _pipe;

        internal DoAppend( Seq<T> pipe ) => _pipe = pipe;

        /// <summary>
        /// Pipe forward operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe
        /// </summary>
        public static DoAppend<T> operator |(DoAppend<T> doAppend, [NotNull] IEnumerable<T> rhs) {
            if (rhs == null) throw new ArgumentNullException(nameof(rhs));

            return new DoAppend<T>(doAppend._pipe | add | rhs);
        }
		
        /// <summary>
        /// Pipe forward operator, adds a new value to IEnumerable{T}.
        /// </summary>
        public static DoAppend<T> operator |(DoAppend<T> doAppend, [CanBeNull] T rhs) => doAppend | Yield(rhs);


        /// <summary>
        /// Pipe forward operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe
        /// </summary>
        public static DoAppend<T> operator |( DoAppend<T> doAppend, Seq<T> rhs ) => doAppend | rhs.get;


        public static Seq<T> operator |( DoAppend<T> doAppend, CommandEnd end )
            => doAppend._pipe;

        
        private static IEnumerable<T> Yield([CanBeNull] T item) {
            yield return item;
        }
    }
}