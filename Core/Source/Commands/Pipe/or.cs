using System;
using System.ComponentModel;
using SharpPipe.Transformations;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// If the pipe is empty replaces contents of the pipe with object on the right.
        /// </summary>
        public static DoOr<T> or<T>(Option<T> alternative) => new DoOr<T>(alternative);
        public static DoOr<T> or<T>(T alternative) => new DoOr<T>(alternative.ToOption());
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoOr<T>
    {
        private readonly Option<T> _alternative;
        
        public DoOr( Option<T> alternative ) => _alternative = alternative;

        public static Pipe<T> operator |( Pipe<T> pipe, DoOr<T> doOr )
            {
                Func<Option<T>, Option<T>> func = i => i.HasValue ? i : doOr._alternative;

                return pipe.Map(func);
            }
    }
}