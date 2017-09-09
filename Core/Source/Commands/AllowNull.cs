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
    
    partial struct EnumPipe<T> {
        public static EnumPipe<T> operator |( EnumPipe<T> pipe, DoAllowNull doAllowNull ) {
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