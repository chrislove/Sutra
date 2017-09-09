using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    /// <summary>
    /// Pipe commands.
    /// </summary>
    public static partial class Commands {
        public static partial class TO {
            public static class STRING {
                public static DoToPipe<string> PIPE => TO<string>.PIPE;
            }
            
            public static class INT {
                public static DoToPipe<int> PIPE    => TO<int>.PIPE;
            }
            
            public static class FLOAT {
                public static DoToPipe<float> PIPE  => TO<float>.PIPE;
            }
            
            public static class DOUBLE {
                public static DoToPipe<double> PIPE => TO<double>.PIPE;
            }
            
            public static class DATETIME {
                public static DoToPipe<DateTime> PIPE => TO<DateTime>.PIPE;
            }
        }
        
        public static class TO<T> {
            /// <summary>
            /// Converts a value to pipe.
            /// </summary>
            /// <example><code>
            /// "TEST" | TO{string}.PIPE
            /// </code></example>
            public static DoToPipe<T> PIPE => new DoToPipe<T>();
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
        /// Converts IEnumerable{T} to EnumerablePipe
        /// </summary>
        public static EnumerablePipe<T> operator |( [NotNull] IEnumerable<T> enumerable, DoToPipe<T> doToPipe ) => new EnumerablePipe<T>(enumerable);
        
        /// <summary>
        /// Initializes EnumerablePipe with object on the right
        /// </summary>
        public static EnumerablePipe<T> operator |( DoToPipe<T> doToPipe, [NotNull] IEnumerable<T> enumerable ) => new EnumerablePipe<T>(enumerable);
    }
}