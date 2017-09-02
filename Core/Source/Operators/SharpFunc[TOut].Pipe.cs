using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed partial class SharpFunc<TOut> {
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static GetPipe<TOut> operator |( [NotNull] GetPipe lhs, [NotNull] SharpFunc<TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			return GetPipe.FromFunc(lhs.Func + rhs);
		}
	}
}