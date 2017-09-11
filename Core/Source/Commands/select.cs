using System.ComponentModel;
using System.Linq;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Transforms pipe contents using a function on the right.
        /// </summary>
        /// <example><code>
        /// pipe | select | (i => i + 1)
        /// </code></example>
        public static DoSelect select => new DoSelect();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoSelect { }

    public partial struct PipeFunc<TIn, TOut> {
        public static Seq<TOut> operator |( DoSelectSeq<TIn> doSelect, PipeFunc<TIn, TOut> func )
            => doSelect.Pipe.Transform( i => i.Select(func.Func) );
    }
}