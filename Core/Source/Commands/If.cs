// ReSharper disable InconsistentNaming

using System.ComponentModel;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Evaluates to true if any of the EnumerablePipe elements match the predicate on the right.
        /// </summary>
        /// <example><code>
        /// pipe | THROW | IFANY | ISNULL;
        /// </code></example>
        public static DoIfAny IFANY => new DoIfAny();
        
        /// <summary>
        /// Evaluates to true if the pipe contents match the predicate on the right.
        /// </summary>
        /// <example><code>
        /// </code></example>
        public static DoIf IF => new DoIf();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoIfAny {}
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoIf {}
}