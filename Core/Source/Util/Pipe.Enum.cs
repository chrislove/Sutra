using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    public static partial class Pipe {
        /// <summary>
        /// Creates and initializes EnumPipe{T} with objects.
        /// </summary>
        public static EnumPipe<TOut> ENUM<TOut>( [NotNull] params TOut[] objs ) => EnumPipe.FromEnumerable(objs);

        /// <summary>
        /// Creates and initializes EnumPipe{T} with an IEnumerable{T}.
        /// </summary>
        public static EnumPipe<TOut> ENUM<TOut>( [NotNull] IEnumerable<TOut> obj ) => EnumPipe.FromEnumerable(obj);

        /// <summary>
        /// Creates an empty EnumPipe{T}
        /// </summary>
        public static EnumPipe<TOut> ENUM<TOut>() => EnumPipe.FromEnumerable(Enumerable.Empty<TOut>());

        public static EnumInFunc<TIn, TOut> ENUM<TIn, TOut>( [CanBeNull] Func<IEnumerable<TIn>, TOut> func ) => EnumInFunc.FromFunc(func);
    }
}