using System.ComponentModel;
using System.Linq;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Converts the contents of EnumerablePipe into T[]
        /// </summary>
        public static DoToArray TOARRAY => new DoToArray();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoToArray {}

    public partial struct EnumerablePipe<T> {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static Pipe<T[]> operator |( EnumerablePipe<T> pipe, DoToArray act ) => START<T[]>.PIPE | pipe.Get.ToArray();
    }
}