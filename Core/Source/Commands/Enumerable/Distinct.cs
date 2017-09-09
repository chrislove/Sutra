using System.ComponentModel;
using System.Linq;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Filters EnumerablePipe to return distinct values
        /// </summary>
        public static DoDistinct DISTINCT => new DoDistinct();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoDistinct { }
    
    public partial struct EnumerablePipe<T> {
        /// <summary>
        /// Pipe forward operator.
        /// </summary>
        public static EnumerablePipe<T> operator |( EnumerablePipe<T> pipe, DoDistinct act ) => START<T>.PIPE | pipe.Get.Distinct();
    }
}