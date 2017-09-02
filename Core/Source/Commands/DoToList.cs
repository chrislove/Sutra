using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Pipe;

namespace SharpPipe {
    public static partial class Pipe {
        public static DoToList TOLIST => new DoToList();
    }
    
    public struct DoToList {
        /// <summary>
        /// Pipe decomposition operator.
        /// Returns the value contained within Pipe{List{T}}
        /// </summary>
        public static ToValue operator ~( DoToList x ) {
            return new ToValue();
        }

        public struct ToValue {}
    }

    public partial class EnumPipe<TOut> {
        /// <summary>
        /// Converts pipe contents into List{TOut}
        /// </summary>
        [NotNull]
        public static Pipe<List<TOut>> operator |( EnumPipe<TOut> lhs, DoToList act ) => IN(lhs.Get.ToList());
        
        /// <summary>
        /// Converts pipe contents into List{TOut}
        /// </summary>
        [NotNull]
        public static List<TOut> operator |( EnumPipe<TOut> lhs, DoToList.ToValue act ) => lhs | TOLIST | OUT;
    }
}