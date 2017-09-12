using System;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Filters contents of Sequence
        /// </summary>
        public static DoWhere where => new DoWhere();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoWhere { }

    public partial struct Seq<T> {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static DoWhere<T> operator |( Seq<T> seq, DoWhere _ ) => new DoWhere<T>(seq);
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoWhere<T> {
        private readonly Seq<T> Seq;

        internal DoWhere( Seq<T> pipe ) => Seq = pipe;

        public static Seq<T> operator |( DoWhere<T> where, [NotNull] Func<T, bool> predicate )
                    => where.Seq | (enm => enm.Where(predicate));
    }
}