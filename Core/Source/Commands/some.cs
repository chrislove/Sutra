using System.ComponentModel;
using JetBrains.Annotations;

namespace Sutra {
    public static partial class Commands
    {
        /// <summary>
        /// Converts a value on the left to Some{T}. Unsafe, will throw if the value on the left is null or none.
        /// Usage: "ABC" | some
        /// </summary>
        public static DoToSome some => new DoToSome();
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoToSome
    {
        public static somestr operator |( [NotNull] string str, DoToSome _ ) => str.ToSome();
        public static somestr operator |( [NotNull] IOption<string> str, DoToSome _ ) => new somestr(str);
    }
}