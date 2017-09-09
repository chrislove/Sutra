using System.Linq;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        public static DoSingle SINGLE => new DoSingle();
    }
    
    public struct DoSingle {}
    
    public partial struct EnumPipe<TOut> {
        public static Pipe<TOut> operator |( EnumPipe<TOut> pipe, DoSingle @do ) => pipe.Get.Single() | TO<TOut>.PIPE;
    }
}