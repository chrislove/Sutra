using System;
using System.Collections.Generic;
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

        public static Seq<T> operator |( DoWhere<T> cmd, [NotNull] Func<IOption, bool> predicate )
        {
            Func<IEnumerable<IOption>, IEnumerable<Option<T>>> func = enm => enm.Where(predicate).Cast<Option<T>>();
            return cmd.Seq.Map(func);
        }
    }
}