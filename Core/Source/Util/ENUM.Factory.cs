using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    /// <summary>
    /// EnumPipe{T} factory
    /// </summary>
    public static class ENUM {
        /// <summary>
        /// Creates and initializes EnumPipe{T} with an IEnumerable{T}.
        /// </summary>
        public static EnumPipe<TOut> IN<TOut>( [NotNull] IEnumerable<TOut> obj ) => EnumPipe.FromEnumerable(obj);

        /// <summary>
        /// Creates an empty EnumPipe{T}
        /// </summary>
        public static EnumPipe<TOut> NEW<TOut>() => EnumPipe.FromEnumerable(Enumerable.Empty<TOut>());

        /// <summary>
        /// Creates an empty EnumPipe{string}
        /// </summary>
        public static EnumPipe<string> STR => NEW<string>();
        
        /// <summary>
        /// Creates an empty EnumPipe{int}
        /// </summary>
        public static EnumPipe<int> INT => NEW<int>();
        
        /// <summary>
        /// Creates an empty EnumPipe{float}
        /// </summary>
        public static EnumPipe<float> FLOAT => NEW<float>();
        
        /// <summary>
        /// Creates an empty EnumPipe{double}
        /// </summary>
        public static EnumPipe<double> DBL => NEW<double>();
    }
}