using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial struct SharpFunc<TIn, TOut> {
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static IOutFunc<TOut> operator +( [NotNull] IOutFunc<TIn> lhs, SharpFunc<TIn, TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));

			Func<object, TIn> lhsFunc = lhs.Func;
			Func<TIn, TOut> rhsFunc   = rhs.Func;

			TOut CombinedFunc( TIn i ) => rhsFunc( lhsFunc(i) );

			return SharpFunc.FromFunc<TIn, TOut>(CombinedFunc);
		}

		/// <summary>
		/// Function composition operator
		/// </summary>
		public static IOutFunc<TOut> operator +( [NotNull] Func<object, TIn> lhs, SharpFunc<TIn, TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));

			TOut CombinedFunc( object i ) => rhs.Func(lhs(i));

			return SharpFunc.FromFunc(CombinedFunc);
		}

		/// <summary>
		/// Function composition operator
		/// </summary>
		public static SharpAct<TIn> operator +( SharpFunc<TIn, TOut> lhs, [NotNull] Action<TOut> rhs ) {
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			void Combined( TIn obj ) => rhs(lhs.Func(obj));

			return SharpAct.FromAction<TIn>(Combined);
		}
	}
}