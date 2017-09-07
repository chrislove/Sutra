using System.Linq;

namespace SharpPipe {
    public static partial class Commands {
        // ReSharper disable once InconsistentNaming
        public static DoDistinct DISTINCT => new DoDistinct();
    }
    
    public struct DoDistinct { }
    
    public partial struct EnumPipe<TOut> {
        /// <summary>
        /// Pipe forward operator.
        /// </summary>
        public static EnumPipe<TOut> operator |( EnumPipe<TOut> pipe, DoDistinct act ) => ENUM.IN(pipe.Get.Distinct());
    }
}