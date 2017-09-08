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

    public partial struct Pipe<TOut> {
        public static ActPipe<TOut> operator |( Pipe<TOut> pipe, ToActPipe rhs )        => new ActPipe<TOut>(pipe.Func);
    }
}