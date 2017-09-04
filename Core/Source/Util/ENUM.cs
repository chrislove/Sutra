using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    /// <summary>
    /// EnumPipe factory
    /// </summary>
    public static class ENUM {
        /// <summary>
        /// Creates and initializes EnumPipe{T} with objects.
        /// </summary>
        //public static EnumPipe<TOut> IN<TOut>( [NotNull] params TOut[] objs ) => EnumPipe.FromEnumerable(objs);

        /// <summary>
        /// Creates and initializes EnumPipe{T} with an IEnumerable{T}.
        /// </summary>
        public static EnumPipe<TOut> IN<TOut>( [NotNull] IEnumerable<TOut> obj ) => EnumPipe.FromEnumerable(obj);

        /// <summary>
        /// Creates an empty EnumPipe{T}
        /// </summary>
        public static EnumPipe<TOut> IN<TOut>() => EnumPipe.FromEnumerable(Enumerable.Empty<TOut>());

        /// <summary>
        /// Creates an empty EnumPipe{string}
        /// </summary>
        public static EnumPipe<string> STR => ENUM.IN<string>();
        
        /// <summary>
        /// Creates an empty EnumPipe{int}
        /// </summary>
        public static EnumPipe<int> INT => ENUM.IN<int>();
        
        /// <summary>
        /// Creates an empty EnumPipe{float}
        /// </summary>
        public static EnumPipe<float> FLOAT => ENUM.IN<float>();
        
        /// <summary>
        /// Creates an empty EnumPipe{double}
        /// </summary>
        public static EnumPipe<double> DBL => ENUM.IN<double>();
    }
}