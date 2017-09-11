using System.ComponentModel;



namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Allows a pipe to return null value.
        /// </summary>
        public static DoAllowNull allownull => new DoAllowNull();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAllowNull { }
    
    partial struct Seq<T> {
        public static Seq<T> operator |( Seq<T> pipe, DoAllowNull doAllowNull ) {
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