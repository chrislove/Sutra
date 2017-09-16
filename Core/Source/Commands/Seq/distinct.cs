using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Filters sequence to return distinct values
        /// </summary>
        public static DoDistinct distinct => new DoDistinct();
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoDistinct { }
    
    public partial struct Seq<T> {
        /// <summary>
        /// Selects distinct values from the sequence.
        /// </summary>
        public static Seq<T> operator |( Seq<T> seq, DoDistinct _ ) => seq.Option.Map( ( IEnumerable<Option<T>> enm ) => enm?.Distinct() );
    }
}