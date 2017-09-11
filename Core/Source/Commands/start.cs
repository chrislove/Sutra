using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;

namespace SharpPipe {
    public static partial class Commands {
        public static class start<T> {
            public static DoStartPipe<T> pipe => new DoStartPipe<T>();
        }

        public static class start {
            /// <summary>
            /// Starts a {string} pipe.
            /// </summary>
            public static class str {
                public static DoStartPipe<string> pipe => start<string>.pipe;
            }

            /// <summary>
            /// Starts a {int} pipe.
            /// </summary>
            public static class integer {
                public static DoStartPipe<int> pipe => start<int>.pipe;
            }

            /// <summary>
            /// Starts a {float} pipe.
            /// </summary>
            public static class flt {
                public static DoStartPipe<float> pipe => start<float>.pipe;
            }

            /// <summary>
            /// Starts a {double} pipe.
            /// </summary>
            public static class dbl {
                public static DoStartPipe<double> pipe => start<double>.pipe;
            }
        }
    }
        
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct DoStartPipe<T> {
        /// <summary>
        /// Initializes a pipe with object on the right
        /// </summary>
        public static Pipe<T> operator |( DoStartPipe<T> doStartPipe, [NotNull] T obj ) => Pipe.From(obj);
        
        /// <summary>
        /// Initializes a sequence with enumerable on the right
        /// </summary>
        public static Seq<T> operator |( DoStartPipe<T> doStartPipe, [NotNull] IEnumerable<T> enumerable ) => Pipe.From(enumerable);
    }
}