using System.ComponentModel;
using System.Linq;
using static SharpPipe.Commands;



namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Converts the contents of Sequence into T[] and returns.
        /// </summary>
        public static DoToArray retarray => new DoToArray();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoToArray {}

    public partial struct Seq<T> {
        /// <summary>
        /// Converts pipe contents into TOut[] and returns.
        /// </summary>
        public static T[] operator |( Seq<T> pipe, DoToArray act ) => start<T[]>.pipe | pipe.Get.ToArray() | Commands.ret;
    }
}