using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed partial class SharpFunc<TOut> {
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static Pipe<TOut> operator |( [NotNull] IPipe lhs, [NotNull] SharpFunc<TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			return Pipe.FromFunc(lhs.Func + rhs);
		}
	}
}