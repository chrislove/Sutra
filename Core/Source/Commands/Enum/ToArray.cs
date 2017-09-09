using System.ComponentModel;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Converts the contents of EnumerablePipe{T} into T[]
        /// </summary>
        public static DoToArray TOARRAY => new DoToArray();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoToArray {}

    public partial struct EnumPipe<T> {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static Pipe<T[]> operator |( EnumPipe<T> pipe, DoToArray act ) => NEW<T[]>.PIPE | pipe.Get.ToArray();
    }
}