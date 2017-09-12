using System;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    public partial struct Seq<T> {
        public static DoMapSeq<T> operator |( Seq<T> seq, DoMap _ ) => new DoMapSeq<T>(seq);
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoMapSeq<T> {
        internal readonly Seq<T> Seq;

        internal DoMapSeq( Seq<T> pipe ) => Seq = pipe;

        /// <summary>
        /// Sequence select operator.
        /// </summary>
        public static Seq<T> operator |( DoMapSeq<T> doMap, [NotNull] Func<T, T> func )
            => doMap.Seq | (enm => enm.Select(func));
    }

}