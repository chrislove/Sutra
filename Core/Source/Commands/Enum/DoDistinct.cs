using System.Linq;

namespace SharpPipe {
    public static partial class Pipe {
        // ReSharper disable once InconsistentNaming
        public static DoDistinct DISTINCT => new DoDistinct();
    }
    
    public struct DoDistinct { }
    
    public partial struct EnumPipe<TOut> {
        /// <summary>
        /// Pipe forward operator.
        /// </summary>
        public static EnumPipe<TOut> operator |( EnumPipe<TOut> lhs, DoDistinct act ) => ENUM.IN(lhs.Get.Distinct());
    }
}