using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static SharpPipe.Commands;



namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Converts the contents of Sequence into List{T} and returns.
        /// </summary>
        public static DoToList retlist => new DoToList();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoToList {}

    public partial struct Seq<T> {
        /// <summary>
        /// Converts pipe contents into List{TOut} and returns
        /// </summary>
        public static List<T> operator |( Seq<T> pipe, DoToList act ) => start<List<T>>.pipe | pipe.Get.ToList() | Commands.ret;
    }
}