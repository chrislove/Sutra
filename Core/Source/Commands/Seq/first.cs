using System.ComponentModel;
using System.Linq;

namespace Sutra {
    public static partial class Commands {
        /// <summary>
        /// Returns the first item of sequence.
        /// </summary>
        public static DoFirst first => new DoFirst();
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoFirst {}
    
    public partial struct Seq<T> {
        public static Pipe<T> operator |( Seq<T> seq, DoFirst _ ) => seq | (enm => enm.First());
    }
}