using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed partial class SharpFunc<TOut> {
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc<TOut> operator +( [NotNull] SharpFunc lhs, [NotNull] SharpFunc<TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			return (i => lhs.Func(i)) + rhs;
		}

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc<TOut> operator +( [NotNull] Func<object, object> lhs, [NotNull] SharpFunc<TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not possible

			return SharpFunc.FromFunc(
			                        i => rhs.Func(lhs(i))
			                       );
		}

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpAct<TOut> operator +( [NotNull] SharpFunc<TOut> lhs, [NotNull] Action<TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not needed

			return SharpAct.FromAction<TOut>(
			                                 i => rhs(lhs.Func(i))
			                                );
		}

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpAct<TOut> operator +( [NotNull] SharpFunc<TOut> lhs, [NotNull] SharpAct<TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not needed

			return lhs + rhs.Action;
		}
	}
}