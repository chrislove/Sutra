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
    
    partial struct EnumerablePipe<T> {
        public static EnumerablePipe<T> operator |( EnumerablePipe<T> pipe, DoAllowNull doAllowNull ) {
            pipe.AllowNullOutput = true;
            return pipe;
        }
    }

    public partial struct Pipe<T> {
        public static Pipe<T> operator |( Pipe<T> pipe, DoAllowNull doAllowNull )  {
            pipe.AllowNullOutput = true;
            return pipe;
        }
    }
}