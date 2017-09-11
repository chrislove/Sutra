using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;



namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Adds a single item to Sequence.
        /// </summary>
        public static DoAdd add => new DoAdd();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAdd { }

    public partial struct Seq<T> {
        public static DoAdd<T> operator |( Seq<T> pipe, DoAdd doAdd ) => new DoAdd<T>( pipe );
    }

    public partial struct DoToPipe<T> {
        /// <summary>
        /// Starts a enumerable pipe
        /// </summary>
        public static DoAdd<T> operator |( DoToPipe<T> pipe, DoAdd doAdd ) => new DoAdd<T>( Seq<T>.empty );
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAdd<T> {
        private readonly Seq<T> _pipe;

        internal DoAdd( Seq<T> lhs ) => _pipe = lhs;
        
        /// <summary>
        /// Pipe forward operator, concatenates two IEnumerable{T} and returns a new Sequence
        /// </summary>
        public static Seq<T> operator |(DoAdd<T> lhs, [NotNull] IEnumerable<T> rhs) {
            if (rhs == null) throw new ArgumentNullException(nameof(rhs));

            return lhs._pipe.Get.Concat(rhs) | to<T>.pipe;
        }
		
        /// <summary>
        /// Pipe forward operator, adds a new value to IEnumerable{T}.
        /// </summary>
        public static Seq<T> operator |(DoAdd<T> lhs, [CanBeNull] T rhs) => lhs | Yield(rhs);


        /// <summary>
        /// Pipe forward operator, concatenates two IEnumerable{T} and returns a new Sequence
        /// </summary>
        public static Seq<T> operator |( DoAdd<T> lhs, Seq<T> rhs ) => lhs | rhs.Get;

        private static IEnumerable<T> Yield([CanBeNull] T item) {
            yield return item;
        }
    }
}