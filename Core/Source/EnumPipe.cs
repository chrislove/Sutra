using System;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace SharpPipe
{
	public static class EnumPipe {
		[NotNull]
		internal static Action ForEachAction<T>( [NotNull] this IEnumerable<T> enumerable, [NotNull] Action<T> act ) {
			if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
			if (act == null) throw new ArgumentNullException(nameof(act));
			
			return () => {
				       if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

				       foreach (var item in enumerable) act(item);
			       };
		}
	}
}