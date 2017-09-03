using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Pipe;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Pipe {
        public static DoToArray TOARRAY => new DoToArray();
    }
    
    public struct DoToArray {}

    public partial struct EnumPipe<TOut> {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static Pipe<TOut[]> operator |( EnumPipe<TOut> lhs, DoToArray act ) => IN(lhs.Get.ToArray());
    }
}