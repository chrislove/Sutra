using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;

namespace SharpPipe {
    /// <summary>
    /// Pipe commands.
    /// </summary>
    public static partial class Commands {
        public static partial class to {
            public static class str {
                public static DoToPipe<string> pipe => to<string>.pipe;
            }
            
            public static class integer {
                public static DoToPipe<int> pipe    => to<int>.pipe;
            }
            
            public static class flt {
                public static DoToPipe<float> pipe  => to<float>.pipe;
            }
            
            public static class dbl {
                public static DoToPipe<double> pipe => to<double>.pipe;
            }
        }
        
        public static class to<T> {
            /// <summary>
            /// Converts a value to pipe.
            /// </summary>
            /// <example><code>
            /// "TEST" | TO{string}.PIPE
            /// </code></example>
            public static DoToPipe<T> pipe => new DoToPipe<T>();
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct DoToPipe<T> {
        /// <summary>
        /// Converts object to Pipe{T}
        /// </summary>
        public static Pipe<T> operator |( [NotNull] T obj, DoToPipe<T> doToPipe ) => new Pipe<T>(obj);
        
        /// <summary>
        /// Initializes Pipe{T} with object on the right
        /// </summary>
        public static Pipe<T> operator |( DoToPipe<T> doToPipe, [NotNull] T obj ) => new Pipe<T>(obj);
        
        /// <summary>
        /// Converts IEnumerable{T} to Sequence
        /// </summary>
        public static Seq<T> operator |( [NotNull] IEnumerable<T> enumerable, DoToPipe<T> doToPipe ) => new Seq<T>(enumerable);
        
        /// <summary>
        /// Initializes Sequence with object on the right
        /// </summary>
        public static Seq<T> operator |( DoToPipe<T> doToPipe, [NotNull] IEnumerable<T> enumerable ) => new Seq<T>(enumerable);
    }
}