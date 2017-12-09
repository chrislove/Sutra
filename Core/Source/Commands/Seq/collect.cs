using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace Sutra {
    public static partial class Commands {
        /// <summary>
        /// Projects each element of a sequence and flattens the resulting sequences into one sequence.
        /// Equivalent to SelectMany() in LINQ.
        /// </summary>
        /// <list type="bullet">
        /// <item><description>For Seq{T}: Projects each element of a sequence and flattens the resulting sequences into one sequence. Equivalent to SelectMany() in LINQ.</description></item>
        /// <item><description>For Pipe{T}: Transforms pipe to a sequence.</description></item>
        /// </list>
        /// <example>
        /// <code>
        /// seq | collect | (i => Enumerable.Repeat(i, 3))
        /// </code>
        /// </example>
        public static DoCollect collect => new DoCollect();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoCollect { }
    
    public partial struct Seq<T> {
        public static DoCollect<T> operator |( Seq<T> seq, DoCollect _ ) => new DoCollect<T>(seq);
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoCollect<T> {
        private readonly Seq<T> _seq;

        internal DoCollect( Seq<T> seq ) => _seq = seq;

        public static Seq<T> operator |( DoCollect<T> doSelect, [NotNull] Func<T, IEnumerable<T>> func )
            => doSelect._seq | (enm => enm.SelectMany(func));
    }
}