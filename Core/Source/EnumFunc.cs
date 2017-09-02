using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
	public static class EnumFunc {
		[NotNull]
		public static EnumFunc<TIn, TOut> FromFunc<TIn, TOut>(Func<TIn, IEnumerable<TOut>> func) => new EnumFunc<TIn, TOut>(func);
	}
}