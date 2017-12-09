using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Sutra
{
    internal static class FuncExtensions
    {
        /// <summary>
        /// Returns an empty enumerable if the input is null.
        /// </summary>
        [ContractAnnotation("null => notnull; notnull => notnull")]
        public static IEnumerable<T> EmptyIfNull<T>( [CanBeNull] this IEnumerable<T> enm )
            => enm ?? Enumerable.Empty<T>();
    }
}