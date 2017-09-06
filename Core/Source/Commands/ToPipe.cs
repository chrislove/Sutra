using System.Collections.Generic;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Converts an object into Pipe{T} or converts an IEnumerable{T} into EnumPipe{T}
        /// </summary>
        public static DoToPipe<string> TOSTR => new DoToPipe<string>();

        /*
        public static class TO<T> {
            /// <summary>
            /// Converts a value on the left to Pipe{T}
            /// </summary>
            public static DoToPipe<T> PIPE => new DoToPipe<T>();
        }*/
        
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
        
        /// <summary>
        /// Converts T[] to EnumPipe{T}
        /// </summary>
        public static EnumPipe<T> operator |( [NotNull] T[] enumerable, DoToPipe<T> rhs ) => ENUM.IN(enumerable);
    }
}