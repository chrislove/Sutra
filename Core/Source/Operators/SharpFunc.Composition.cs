using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public abstract partial class SharpFunc {
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpAct<object> operator +( [NotNull] SharpFunc lhs, [NotNull] Action<object> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not needed

			return SharpAct.FromAction(lhs.Func.CombineWith(rhs));
		}
	}
}