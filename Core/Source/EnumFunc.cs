using System;
using System.Collections.Generic;

namespace SharpPipe {
	public static class EnumFunc {
		public static EnumFunc<TIn, TOut> FromFunc<TIn, TOut>(Func<TIn, IEnumerable<TOut>> func) => new EnumFunc<TIn, TOut>(func);
	}
}