using System.ComponentModel;
using System.Linq;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Returns the first item of EnumerablePipe.
        /// </summary>
        public static DoFirst FIRST => new DoFirst();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoFirst {}
    
    public partial struct EnumerablePipe<T> {
        public static Pipe<T> operator |( EnumerablePipe<T> pipe, DoFirst @do ) => pipe.Get.First() | TO<T>.PIPE;
    }
}