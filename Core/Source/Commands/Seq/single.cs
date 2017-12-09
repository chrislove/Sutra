using System.ComponentModel;
using System.Linq;

namespace Sutra {
    public static partial class Commands {
        /// <summary>
        /// Returns a single value from a sequence or throws an excepton.
        /// </summary>
        public static DoSingle single => new DoSingle();
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoSingle {}
    
    public partial struct Seq<T> {
        public static Pipe<T> operator |( Seq<T> seq, DoSingle _ )
            => seq | (enm => enm.Single());
    }
}