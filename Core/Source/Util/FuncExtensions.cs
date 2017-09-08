﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
	internal static class FuncExtensions {
		[NotNull]
		public static T NotNull<T>( [CanBeNull] this T obj, Type context ) {
			if (obj == null)
				throw PipeNullException.ForObject<T>(context);

			return obj;
		}
		
		[NotNull]
		public static Action ForEachAction<T>( [NotNull] this IEnumerable<T> enumerable, [NotNull] Action<T> act ) {
			if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
			if (act == null) throw new ArgumentNullException(nameof(act));
			
			return () => {
				       if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

				       foreach (var item in enumerable) act(item);
			       };
		}
	}
}