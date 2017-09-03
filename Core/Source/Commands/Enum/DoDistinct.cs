using System.Linq;
using static SharpPipe.Pipe;

namespace SharpPipe {
    public static partial class Pipe {
        public static DoDistinct DISTINCT => new DoDistinct();
    }
    
    public struct DoDistinct { }
    
    public partial struct EnumPipe<TOut> {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static EnumPipe<TOut> operator |( EnumPipe<TOut> lhs, DoDistinct act ) => ENUM(lhs.Get.Distinct());
    }
}