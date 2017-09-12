using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;


namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Adds a single item to sequence.
        /// </summary>
        public static DoAdd add => new DoAdd();
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAdd { }

    public partial struct Seq<T> {
        public static DoAdd<T> operator |( Seq<T> seq, DoAdd _ ) => new DoAdd<T>( seq );
    }

    public partial struct DoStartSeq<T> {
        /// <summary>
        /// Starts a new sequence.
        /// </summary>
        public static DoAdd<T> operator |( DoStartSeq<T> cmd, DoAdd _ ) => new DoAdd<T>( Seq<T>.Empty );
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAdd<T> {
        private readonly Seq<T> _seq;

        internal DoAdd( Seq<T> seq ) => _seq = seq;

        /// <summary>
        /// Pipe forward operator, concatenates sequence with IEnumerable{T} and returns a new sequence.
        /// </summary>
        public static Seq<T> operator |( DoAdd<T> doAdd, [CanBeNull] IEnumerable<T> enm ) => doAdd._seq | enm;

        /// <summary>
        /// Pipe forward operator, adds a new value to a sequence.
        /// </summary>
        public static Seq<T> operator |( DoAdd<T> doAdd, [CanBeNull] T obj ) => doAdd._seq | obj;

        /// <summary>
        /// Pipe forward operator, concatenates two sequences and returns a new sequence.
        /// </summary>
        public static Seq<T> operator |( DoAdd<T> doAdd, Seq<T> seq ) {
            foreach (var value in seq.Option)
                return doAdd._seq | value;

            return Seq<T>.SkipSeq;
        }
    }
}