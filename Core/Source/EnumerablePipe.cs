using JetBrains.Annotations;
using System.Collections.Generic;

namespace SharpPipe
{
	public static class EnumerablePipe {
		[NotNull]
		internal static EnumerablePipe<T> FromEnumerable<T>([NotNull] IEnumerable<T> enumerable) => new EnumerablePipe<T>(enumerable);
	}
}