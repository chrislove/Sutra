using System.Collections.Generic;
using System.Linq;

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
        public static Pipe<List<TOut>> operator |( EnumPipe<TOut> pipe, DoToList act ) => NEW<List<TOut>>.PIPE | pipe.Get.ToList();
    }
}