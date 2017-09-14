

using System.ComponentModel;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Evaluates to true if the pipe contents match the predicate on the right.
        /// </summary>
        /// <example><code>
        /// </code></example>
        public static DoWhen when => new DoWhen();
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoWhen {}
}