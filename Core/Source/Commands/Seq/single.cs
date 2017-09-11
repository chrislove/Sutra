using System.ComponentModel;
using System.Linq;
using static SharpPipe.Commands;



namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Returns a single value from Sequence or throws an excepton.
        /// </summary>
        public static DoSingle single => new DoSingle();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoSingle {}
    
    public partial struct Seq<T> {
        public static Pipe<T> operator |( Seq<T> pipe, DoSingle @do ) => pipe.Get.Single() | to<T>.pipe;
    }
}