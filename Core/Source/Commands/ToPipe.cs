using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Converts T => Pipe{T} or IEnumerable{T} => EnumPipe{T}
        /// </summary>

        public static class TO {
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
            public static DoToPipe<T> PIPE => new DoToPipe<T>();
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoToPipe<T> {
        /// <summary>
        /// Converts object to Pipe{T}
        /// </summary>
        public static Pipe<T> operator |( [NotNull] T obj, DoToPipe<T> rhs ) => new Pipe<T>(obj);
        
        /// <summary>
        /// Converts IEnumerable{T} to EnumPipe{T}
        /// </summary>
        public static EnumPipe<T> operator |( [NotNull] IEnumerable<T> enumerable, DoToPipe<T> rhs ) => new EnumPipe<T>(enumerable);
    }
}