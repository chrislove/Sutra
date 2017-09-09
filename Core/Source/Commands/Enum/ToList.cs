using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Converts the contents of EnumerablePipe{T} into List{T}.
        /// </summary>
        public static DoToList TOLIST => new DoToList();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoToList {}

    public partial struct EnumPipe<T> {
        /// <summary>
        /// Converts pipe contents into List{TOut}
        /// </summary>
        public static Pipe<List<T>> operator |( EnumPipe<T> pipe, DoToList act ) => NEW<List<T>>.PIPE | pipe.Get.ToList();
    }
}