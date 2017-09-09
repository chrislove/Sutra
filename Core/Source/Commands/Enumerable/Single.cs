using System.ComponentModel;
using System.Linq;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Returns a single value from EnumerablePipe or throws an excepton.
        /// </summary>
        public static DoSingle SINGLE => new DoSingle();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoSingle {}
    
    public partial struct EnumerablePipe<T> {
        public static Pipe<T> operator |( EnumerablePipe<T> pipe, DoSingle @do ) => pipe.Get.Single() | TO<T>.PIPE;
    }
}