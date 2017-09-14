using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    internal static class OptionTransformations {
        /// <summary>
        /// Lifts a value to Option{T}
        /// </summary>
        [Pure] public static Option<T>    ToOption<T>              ([CanBeNull] this T obj) => new Option<T>(obj);
        
        /// <summary>
        /// Lifts every value of the enumerable to Option{T}
        /// </summary>
        [CanBeNull]
        [Pure] public static IEnumerable<Option<T>> Return<T>( [CanBeNull] this IEnumerable<T> enm ) => enm?.Select(i => i.ToOption());
        
        [NotNull]
        [Pure] public static Func<Option<T>, Option<U>> Map<T, U>   ( [CanBeNull] this Func<T, U> func ) => i => i.Map(func);

        /// <summary>
        /// Lowers every value of the enumerable to T
        /// </summary>
        [NotNull]
        [Pure] public static IEnumerable<T> Lower<T>( [NotNull] this IEnumerable<Option<T>> enm ) => enm.Select(i => i.ValueOrFail());
    }
}