using System.ComponentModel;
using System.Linq;
using static SharpPipe.Commands;



namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Filters Sequence to return distinct values
        /// </summary>
        public static DoDistinct distinct => new DoDistinct();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoDistinct { }
    
    public partial struct Seq<T> {
        /// <summary>
        /// Pipe forward operator.
        /// </summary>
        public static Seq<T> operator |( Seq<T> pipe, DoDistinct act ) => start<T>.pipe | pipe.Get.Distinct();
    }
}