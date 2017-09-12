using System.ComponentModel;
using System.Linq;
using static SharpPipe.Commands;



namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Filters sequence to return distinct values
        /// </summary>
        public static DoDistinct distinct => new DoDistinct();
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoDistinct { }
    
    public partial struct Seq<T> {
        /// <summary>
        /// Pipe forward operator.
        /// </summary>
        public static Seq<T> operator |( Seq<T> seq, DoDistinct _ ) {
            foreach (var value in seq.Option) {
                return start<T>.seq | value.Distinct();
            }

            return SkipSeq;
        }
    }
}