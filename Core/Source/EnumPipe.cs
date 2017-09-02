using JetBrains.Annotations;
using System.Collections.Generic;

namespace SharpPipe
{
	public static class EnumPipe {
		internal static EnumPipe<T> FromEnumerable<T>([NotNull] IEnumerable<T> enumerable) => new EnumPipe<T>(enumerable);

		internal static EnumPipe<T> FromFunc<T>(IOutFunc<IEnumerable<T>> func) => new EnumPipe<T>(func);
	}
}