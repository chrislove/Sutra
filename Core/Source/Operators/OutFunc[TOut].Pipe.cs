using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed partial class OutFunc<TOut> {
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static GetPipe<TOut> operator |( [NotNull] GetPipe lhs, [NotNull] OutFunc<TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			lhs.ValidateCompatibilityWith(rhs);

			return GetPipe.FromFunc(lhs.Func + rhs);
		}
	}
}