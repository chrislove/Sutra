using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Converts the contents of EnumerablePipe into List{T}.
        /// </summary>
        public static DoToList TOLIST => new DoToList();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoToList {}

    public partial struct EnumerablePipe<T> {
        /// <summary>
        /// Converts pipe contents into List{TOut}
        /// </summary>
        public static Pipe<List<T>> operator |( EnumerablePipe<T> pipe, DoToList act ) => START<List<T>>.PIPE | pipe.Get.ToList();
    }
}