using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    /// <summary>
    /// EnumPipe{T} factory
    /// </summary>
    public static class ENUM<T> {
        /// <summary>
        /// Creates an empty EnumPipe{T}
        /// </summary>
        public static EnumPipe<T> NEW => ENUM.IN(Enumerable.Empty<T>());
    }

    /// <summary>
    /// EnumPipe{T} factory
    /// </summary>
    public static class ENUM {
        /// <summary>
        /// Creates and initializes EnumPipe{T} with an IEnumerable{T}.
        /// </summary>
        public static EnumPipe<TOut> IN<TOut>( [NotNull] IEnumerable<TOut> enumerable ) => new EnumPipe<TOut>(enumerable);

        /// <summary>
        /// Creates an empty EnumPipe{string}
        /// </summary>
        public static EnumPipe<string> STR => ENUM<string>.NEW;

        /// <summary>
        /// Creates an empty EnumPipe{int}
        /// </summary>
        public static EnumPipe<int> INT => ENUM<int>.NEW;
        
        /// <summary>
        /// Creates an empty EnumPipe{float}
        /// </summary>
        public static EnumPipe<float> FLOAT => ENUM<float>.NEW;
        
        /// <summary>
        /// Creates an empty EnumPipe{double}
        /// </summary>
        public static EnumPipe<double> DBL => ENUM<double>.NEW;
    }
}