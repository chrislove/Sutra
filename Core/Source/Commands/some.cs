using System.ComponentModel;

namespace SharpPipe {
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
        public static somestr operator |( string str, DoToSome _ ) => str.Some();
        public static somestr operator |( IOption<string> str, DoToSome _ ) => new somestr(str);
    }
}