using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial class SharpFunc<TIn, TOut> {
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc<TIn, TOut> operator +( [NotNull] IOutFunc<TIn> lhs, [NotNull] SharpFunc<TIn, TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			Func<object, TIn> lhsFunc = lhs.Func;
			Func<TIn, TOut> rhsFunc = rhs.Func;

			TOut CombinedFunc( TIn i ) => rhsFunc(lhsFunc(i));

			return SharpFunc.FromFunc<TIn, TOut>(CombinedFunc);
		}

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc<TOut> operator +( [NotNull] Func<object, TIn> lhs, [NotNull] SharpFunc<TIn, TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			TOut CombinedFunc( object i ) => rhs.Func(lhs(i));

			return SharpFunc.FromFunc(CombinedFunc);
		}

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpAct<TIn> operator +( [NotNull] SharpFunc<TIn, TOut> lhs, [NotNull] Action<TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			void Combined( TIn obj ) => rhs(lhs.Func(obj));

			return SharpAct.FromAction<TIn>(Combined);
		}
	}
}