using System;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace SharpPipe
{
	public static class EnumPipe {
		internal static EnumPipe<T> FromEnumerable<T>([NotNull] IEnumerable<T> enumerable) => new EnumPipe<T>(enumerable);

		internal static EnumPipe<T> FromFunc<T>(SharpFunc<IEnumerable<T>> func) => new EnumPipe<T>(func);
		
		[NotNull]
		internal static Action ForEachAction<T>( [NotNull] this IEnumerable<T> enumerable, Action<T> act ) {
			return () => {
				       if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

				       foreach (var item in enumerable) act(item);
			       };
		}
	}
}