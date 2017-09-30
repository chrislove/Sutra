using System.ComponentModel;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Projects each element of a sequence into a new form using a function on the right.
        /// Equivalent to Select() in LINQ.
        /// </summary>
        /// <example><code>
        /// seq | map | (i => i + 1)
        /// </code></example>
        public static DoMap map => new DoMap();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoMap { }
}