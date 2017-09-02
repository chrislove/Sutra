using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial struct SharpFunc<TOut> {
		/// <summary>
		/// Function composition operator
		/// </summary>
		public static SharpFunc<TOut> operator +( [NotNull] ISharpFunc lhs, SharpFunc<TOut> rhs )
			=> (i => lhs.Func(i)) + rhs;

		/// <summary>
		/// Function composition operator
		/// </summary>
		public static SharpFunc<TOut> operator +( [NotNull] Func<object, object> lhs, SharpFunc<TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));

			return SharpFunc.FromFunc(
			                        i => rhs.Func(lhs(i))
			                       );
		}

		/// <summary>
		/// Function composition operator
		/// </summary>
		public static SharpAct<TOut> operator +( SharpFunc<TOut> lhs, [NotNull] Action<TOut> rhs ) {
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			return SharpAct.FromAction<TOut>(
			                                 i => rhs(lhs.Func(i))
			                                );
		}

		/// <summary>
		/// Function composition operator
		/// </summary>
		public static SharpAct<TOut> operator +( SharpFunc<TOut> lhs, SharpAct<TOut> rhs ) => lhs + rhs.Action;
	}
}