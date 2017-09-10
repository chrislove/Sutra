

using System.ComponentModel;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Evaluates to true if any of the Sequence elements match the predicate on the right.
        /// </summary>
        /// <example><code>
        /// pipe | fail | IFANY | ISNULL;
        /// </code></example>
        public static DoWhenAny whenany => new DoWhenAny();
        
        /// <summary>
        /// Evaluates to true if the pipe contents match the predicate on the right.
        /// </summary>
        /// <example><code>
        /// </code></example>
        public static DoWhen when => new DoWhen();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoWhenAny {}
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoWhen {}
}