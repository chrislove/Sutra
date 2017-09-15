using System;
using System.ComponentModel;
using JetBrains.Annotations;
using SharpPipe.Transformations;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// If the pipe is empty replaces contents of the pipe with object on the right.
        /// </summary>
        public static DoOr or => new DoOr();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoOr { }
    
    public partial struct Pipe<T> {
        public static DoOr<T> operator |( Pipe<T> pipe, DoOr _ ) => new DoOr<T>(pipe);
    }
    
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoOr<T> {
        private readonly Pipe<T> _pipe;

        public DoOr( Pipe<T> pipe ) => _pipe = pipe;

        public static Pipe<T> operator |( DoOr<T> doOr, Option<T> alternative )
            {
                Func<Option<T>, Option<T>> func = i => i.HasValue ? i : alternative;

                return doOr._pipe.Map(func);
            }

        public static Pipe<T> operator |( DoOr<T> doOr, [CanBeNull] T alternative ) => doOr | alternative.ToOption();
    }
}