using System;
using JetBrains.Annotations;

namespace SharpPipe {
	internal static class FuncExtensions {
		[NotNull]
		public static T NotNull<T>( [CanBeNull] this T obj, Type context ) {
			if (obj == null)
				throw PipeNullException.ForObject<T>(context);

			return obj;
		}
	}
}