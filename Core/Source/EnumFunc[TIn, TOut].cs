using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
	public class EnumFunc<TIn, TOut> : SharpFunc<TIn, IEnumerable<TOut>> {
		internal EnumFunc( [NotNull] Func<TIn, IEnumerable<TOut>> func ) : base(func) { }

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static EnumPipe<TOut> operator |(Pipe<TIn> lhs, [NotNull] EnumFunc<TIn, TOut> rhs)
		{
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			return EnumPipe.FromFunc(lhs.Func + rhs);
		}
	}
}