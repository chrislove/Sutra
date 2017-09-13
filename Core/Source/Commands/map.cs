using System.ComponentModel;
using System.Linq;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Projects each element of a sequence into a new form using a function on the right.
        /// Equivalent to Select() in LINQ.
        /// </summary>
        /// <example><code>
        /// seq | sel | (i => i + 1)
        /// </code></example>
        public static DoMap map => new DoMap();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoMap { }

    public partial struct PipeFunc<TIn, TOut> {
        /// <summary>
        /// Projects each element of a sequence into a new form.
        /// </summary>
        public static Seq<TOut> operator |( DoMapSeq<TIn> doMap, PipeFunc<TIn, TOut> func ) {
            foreach (var value in doMap.Seq.Option)
                return start<TOut>.seq | value.Select(func.Func);

            return Seq<TOut>.SkipSeq;
        }
    }
}