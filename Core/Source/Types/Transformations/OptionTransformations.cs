using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe.Transformations {
    public static class OptionTransformations {
        /// <summary>
        /// Lifts a value to Option{T}
        /// </summary>
        [Pure] public static Option<T>    ToOption<T>              ([CanBeNull] this T obj) => new Option<T>(obj);
        
        /// <summary>
        /// Lifts every value of the enumerable to Option{T}
        /// </summary>
        [CanBeNull]
        [Pure] public static IEnumerable<Option<T>> Return<T>( [CanBeNull] this IEnumerable<T> enm ) => enm?.Select(i => i.ToOption());
    }
}