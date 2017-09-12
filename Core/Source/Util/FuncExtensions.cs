using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    internal static class FuncExtensions {
        public static void ForEach<T>( [NotNull] this IEnumerable<T> enumerable, [NotNull] Action<T> act ) {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            if (act == null) throw new ArgumentNullException(nameof(act));

            foreach (var item in enumerable) act(item);
        }

        /// <summary>
        /// Returns an empty enumerable if the input is null.
        /// </summary>
        [ContractAnnotation("null => notnull; notnull => notnull")]
        public static IEnumerable<T> EmptyIfNull<T>( [CanBeNull] this IEnumerable<T> enm )
            => enm ?? Enumerable.Empty<T>();
    }
}