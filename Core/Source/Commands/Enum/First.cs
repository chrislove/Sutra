using System.Linq;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        public static DoFirst FIRST => new DoFirst();
    }
    
    public struct DoFirst {}
    
    public partial struct EnumPipe<T> {
        public static Pipe<T> operator |( EnumPipe<T> pipe, DoFirst @do ) => pipe.Get.First() | TO<T>.PIPE;
    }
}