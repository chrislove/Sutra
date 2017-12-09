using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace Sutra {
    public static partial class Commands {
        /// <summary>
        /// Appends new items to a sequence.
        /// </summary>
        public static DoAppend append => new DoAppend();
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAppend { }

    public partial struct Seq<T> {
        public static DoAppend<T> operator |( Seq<T> seq, DoAppend _ ) => new DoAppend<T>( seq );
    }

    public partial struct DoStartSeq<T> {
        public static DoAppend<T> operator |( DoStartSeq<T> cmd, DoAppend _ ) => new DoAppend<T>( new Seq<T>(Enumerable.Empty<Option<T>>()) );
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAppend<T> {
        private readonly Seq<T> _seq;

        internal DoAppend( Seq<T> seq ) => _seq = seq;

        /// <summary>
        /// Pipe forward operator, concatenates sequence with IEnumerable{T} and returns a new sequence.
        /// </summary>
        public static Seq<T> operator |( DoAppend<T> doAppend, [CanBeNull] IEnumerable<T> enm ) => doAppend._seq | enm;

        /// <summary>
        /// Pipe forward operator, adds a new value to a sequence.
        /// </summary>
        public static Seq<T> operator |( DoAppend<T> doAppend, [CanBeNull] T obj ) => doAppend._seq | obj;
        
        /// <summary>
        /// Pipe forward operator, adds a new value to a sequence.
        /// </summary>
        public static Seq<T> operator |( DoAppend<T> doAppend, Option<T> obj ) => doAppend._seq | obj;

        /// <summary>
        /// Pipe forward operator, concatenates two sequences and returns a new sequence.
        /// </summary>
        public static Seq<T> operator |( DoAppend<T> doAppend, Seq<T> seq ) => doAppend._seq | seq;
    }
}