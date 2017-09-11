using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;



namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Appends a sequence of values to sequence.
        /// Usage: pipe | APPEND | 0 | 1 | 2 | I
        /// </summary>
        public static DoAppend append => new DoAppend();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAppend { }
    
    public partial struct DoStartPipe<T> {
        /// <summary>
        /// Starts a new sequence.
        /// </summary>
        public static DoAppend<T> operator |( DoStartPipe<T> pipe, DoAppend doAdd ) => new DoAppend<T>( Seq<T>.Empty );
    }

    public partial struct Seq<T> {
        public static DoAppend<T> operator |( Seq<T> pipe, DoAppend DoAppend ) => new DoAppend<T>(pipe);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAppend<T> {
        private readonly Seq<T> _pipe;

        internal DoAppend( Seq<T> pipe ) => _pipe = pipe;

        /// <summary>
        /// Appends IEnumerable{T} to a sequence.
        /// </summary>
        public static DoAppend<T> operator |(DoAppend<T> doAppend, [NotNull] IEnumerable<T> enumerable) {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

            return new DoAppend<T>(doAppend._pipe | add | enumerable);
        }
		
        /// <summary>
        /// Adds a new value to sequence.
        /// </summary>
        public static DoAppend<T> operator |(DoAppend<T> doAppend, [CanBeNull] T obj) => doAppend | Yield(obj);


        /// <summary>
        /// Concatenates two sequences.
        /// </summary>
        public static DoAppend<T> operator |( DoAppend<T> doAppend, Seq<T> seq ) => doAppend | seq.Get;


        public static Seq<T> operator |( DoAppend<T> doAppend, CommandEnd end )
            => doAppend._pipe;

        
        private static IEnumerable<T> Yield([CanBeNull] T item) {
            yield return item;
        }
    }
}