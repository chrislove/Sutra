// ReSharper disable InconsistentNaming

using System.ComponentModel;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Signals a pipe to convert to ActPipe.
        /// </summary>
        public static ToActPipe ACT => new ToActPipe();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct ToActPipe {}

    public partial struct Pipe<T> {
        public static ActPipe<T> operator |( Pipe<T> pipe, ToActPipe rhs )        => new ActPipe<T>(pipe.Func);
    }
}