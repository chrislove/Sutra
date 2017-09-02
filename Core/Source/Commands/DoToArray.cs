using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Pipe;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Pipe {
        public static DoToArray TOARRAY => new DoToArray();
    }
    
    public struct DoToArray {
        /// <summary>
        /// Pipe decomposition operator.
        /// Returns the value contained within Pipe{T[]}
        /// </summary>
        public static ToValue operator ~( DoToArray x ) {
            return new ToValue();
        }

        public struct ToValue {}
    }

    public partial struct EnumPipe<TOut> {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static Pipe<TOut[]> operator |( EnumPipe<TOut> lhs, DoToArray act ) => IN(lhs.Get.ToArray());

        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        [NotNull]
        public static TOut[] operator |( EnumPipe<TOut> lhs, DoToArray.ToValue act ) => lhs | TOARRAY | OUT;
    }
}