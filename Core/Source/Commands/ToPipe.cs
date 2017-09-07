using System.Collections.Generic;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Converts T => Pipe{T} or IEnumerable{T} => EnumPipe{T}
        /// </summary>
        public static DoToPipe<T> TO<T>() => new DoToPipe<T>();
    }

    public struct DoToPipe<T> {
        /// <summary>
        /// Converts object to Pipe{T}
        /// </summary>
        public static Pipe<T>     operator |( [NotNull] T obj, DoToPipe<T> rhs ) => PIPE.IN(obj);
        
        /// <summary>
        /// Converts IEnumerable{T} to EnumPipe{T}
        /// </summary>
        public static EnumPipe<T> operator |( [NotNull] IEnumerable<T> enumerable, DoToPipe<T> rhs ) => ENUM.IN(enumerable);
    }
}