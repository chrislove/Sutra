using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;



namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Adds a single item to sequence.
        /// </summary>
        public static DoAdd add => new DoAdd();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAdd { }

    public partial struct Seq<T> {
        public static DoAdd<T> operator |( Seq<T> pipe, DoAdd doAdd ) => new DoAdd<T>( pipe );
    }

    public partial struct DoStartPipe<T> {
        /// <summary>
        /// Starts a new sequence.
        /// </summary>
        public static DoAdd<T> operator |( DoStartPipe<T> pipe, DoAdd doAdd ) => new DoAdd<T>( Seq<T>.Empty );
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAdd<T> {
        private readonly Seq<T> _pipe;

        internal DoAdd( Seq<T> seq ) => _pipe = seq;
        
        /// <summary>
        /// Pipe forward operator, concatenates sequence with IEnumerable{T} and returns a new sequence.
        /// </summary>
        public static Seq<T> operator |(DoAdd<T> doAdd, [NotNull] IEnumerable<T> enumerable) {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

            return start<T>.pipe | doAdd._pipe.Get.Concat(enumerable);
        }
		
        /// <summary>
        /// Pipe forward operator, adds a new value to a sequence.
        /// </summary>
        public static Seq<T> operator |(DoAdd<T> doAdd, [CanBeNull] T obj) => doAdd | Yield(obj);


        /// <summary>
        /// Pipe forward operator, concatenates two sequences and returns a new sequence.
        /// </summary>
        public static Seq<T> operator |( DoAdd<T> doAdd, Seq<T> seq ) => doAdd | seq.Get;

        private static IEnumerable<T> Yield([CanBeNull] T item) {
            yield return item;
        }
    }
}