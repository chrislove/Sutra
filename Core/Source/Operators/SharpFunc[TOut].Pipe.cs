using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial struct SharpFunc<TOut> {
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static Pipe<TOut> operator |( [NotNull] IPipe lhs, SharpFunc<TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));

			return Pipe.FromFunc(lhs.Func + rhs);
		}
	}
}