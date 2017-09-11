using System.ComponentModel;
using System.Linq;
using static SharpPipe.Commands;



namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Returns a single value from sequence or throws an excepton.
        /// </summary>
        public static DoSingle single => new DoSingle();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoSingle {}
    
    public partial struct Seq<T> {
        public static Pipe<T> operator |( Seq<T> pipe, DoSingle @do ) => start<T>.pipe | pipe.Get.Single();
    }
}