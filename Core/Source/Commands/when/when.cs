

using System.ComponentModel;

namespace Sutra {
    public static partial class Commands {
        /// <summary>
        /// Evaluates to true if the pipe or sequence contents match the predicate on the right.
        /// </summary>
        public static DoWhen when => new DoWhen();
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoWhen {}
}