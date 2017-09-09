using System.ComponentModel;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Allows a pipe to return null value.
        /// </summary>
        public static DoAllowNull ALLOWNULL => new DoAllowNull();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAllowNull { }
    
    partial struct EnumPipe<TOut> {
        public static EnumPipe<TOut> operator |( EnumPipe<TOut> pipe, DoAllowNull doAllowNull ) {
            pipe.AllowNullOutput = true;
            return pipe;
        }
    }

    public partial struct Pipe<TOut> {
        public static Pipe<TOut> operator |( Pipe<TOut> pipe, DoAllowNull doAllowNull )  {
            pipe.AllowNullOutput = true;
            return pipe;
        }
    }
}