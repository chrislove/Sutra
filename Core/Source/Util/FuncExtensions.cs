using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
    internal static class FuncExtensions {
        public static void ForEach<T>( [NotNull] this IEnumerable<T> enumerable, [NotNull] Action<T> act ) {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            if (act == null) throw new ArgumentNullException(nameof(act));

            foreach (var item in enumerable) act(item);
        }
    }
}