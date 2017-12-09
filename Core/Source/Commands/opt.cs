using System.ComponentModel;
using Sutra.Transformations;

namespace Sutra {
    public static partial class Commands
    {
        /// <summary>
        /// Converts a value on the left to Option{T}
        /// Usage: "ABC" | opt
        /// </summary>
        public static DoToOption opt => new DoToOption();
    }
    
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoToOption
    {
        public static str operator |( string value, DoToOption _ ) => new str(value);
        public static Option<int> operator |( int value, DoToOption _ ) => value.ToOption();
        public static Option<float> operator |( float value, DoToOption _ ) => value.ToOption();
        public static Option<double> operator |( double value, DoToOption _ ) => value.ToOption();
        public static Option<bool> operator |( bool value, DoToOption _ ) => value.ToOption();
        public static Option<long> operator |( long value, DoToOption _ ) => value.ToOption();
    }
}