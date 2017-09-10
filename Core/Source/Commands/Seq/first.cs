using System.ComponentModel;
using System.Linq;
using static SharpPipe.Commands;



namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Returns the first item of Sequence.
        /// </summary>
        public static DoFirst first => new DoFirst();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoFirst {}
    
    public partial struct Seq<T> {
        public static Pipe<T> operator |( Seq<T> pipe, DoFirst @do ) => pipe.get.First() | to<T>.pipe;
    }
}