using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial struct PipeFunc<TIn, TOut> {
		/// <summary>
		/// Function composition operator
		/// </summary>
		public static PipeFunc<TIn, TOut> operator +( PipeFunc<TIn> lhs, PipeFunc<TIn, TOut> rhs ) {
			Func<object, TIn> lhsFunc = lhs.Func;
			Func<TIn, TOut> rhsFunc   = rhs.Func;

			TOut CombinedFunc( TIn i ) => rhsFunc( lhsFunc(i) );

			return PipeFunc.FromFunc<TIn, TOut>(CombinedFunc);
		}
		
		/// <summary>
		/// Function composition operator. A special case for SharpFunc{T, T}
		/// </summary>
		public static PipeFunc<TIn, TOut> operator +( PipeFunc<TIn, TIn> lhs, PipeFunc<TIn, TOut> rhs ) {
			Func<TIn, TIn> lhsFunc  = lhs.Func;
			Func<TIn, TOut> rhsFunc  = rhs.Func;

			TOut CombinedFunc( TIn i ) => rhsFunc( lhsFunc(i) );

			return PipeFunc.FromFunc<TIn, TOut>(CombinedFunc);
		}

		/// <summary>
		/// Function composition operator
		/// </summary>
		public static PipeFunc<TOut> operator +( [NotNull] Func<object, TIn> lhs, PipeFunc<TIn, TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));

			TOut CombinedFunc( object i ) => rhs.Func(lhs(i));

			return PipeFunc.FromFunc(CombinedFunc);
		}
	}
}