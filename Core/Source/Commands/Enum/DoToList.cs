using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        public static DoToList TOLIST => new DoToList();
    }

    public struct DoToList {}

    public partial struct EnumPipe<TOut> {
        /// <summary>
        /// Converts pipe contents into List{TOut}
        /// </summary>
        public static Pipe<List<TOut>> operator -( EnumPipe<TOut> lhs, DoToList act ) => PIPE.IN(lhs.Get.ToList());
    }
}