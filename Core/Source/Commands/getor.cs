using JetBrains.Annotations;

namespace Sutra {
    public static partial class Commands
    {
        /// <summary>
        /// Returns pipe or sequence contents. Safe, with unsafe variations.
        /// </summary>
        /// <example><code>
        /// [safe]   pipe | get
        /// [unsafe] pipe | !get
        /// [unsafe] seq  | !!get
        /// </code></example>
        public static DoGetOr<T> getor<T>(T alternative) => new DoGetOr<T>(alternative);
    }
    
    public struct DoGetOr<T>
    {
        internal T _alternative;
        internal DoGetOr( [CanBeNull] T alternative ) => _alternative = alternative;
    }
}