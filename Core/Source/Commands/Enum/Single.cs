using System.Linq;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        public static DoSingle SINGLE => new DoSingle();
    }
    
    public struct DoSingle {}
    
    public partial struct EnumPipe<T> {
        public static Pipe<T> operator |( EnumPipe<T> pipe, DoSingle @do ) => pipe.Get.Single() | TO<T>.PIPE;
    }
}