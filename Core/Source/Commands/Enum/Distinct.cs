using System.Linq;

namespace SharpPipe {
    public static partial class Commands {
        // ReSharper disable once InconsistentNaming
        public static DoDistinct DISTINCT => new DoDistinct();
    }
    
    public struct DoDistinct { }
    
    public partial struct EnumPipe<T> {
        /// <summary>
        /// Pipe forward operator.
        /// </summary>
        public static EnumPipe<T> operator |( EnumPipe<T> pipe, DoDistinct act ) => NEW<T>.PIPE | pipe.Get.Distinct();
    }
}