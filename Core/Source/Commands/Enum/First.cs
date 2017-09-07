using System.Linq;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        public static DoFirst FIRST => new DoFirst();
    }
    
    public struct DoFirst {}
    
    public partial struct EnumPipe<TOut> {
        public static Pipe<TOut> operator |( EnumPipe<TOut> pipe, DoFirst @do ) => pipe.Get.First() | TO<TOut>();
    }
}