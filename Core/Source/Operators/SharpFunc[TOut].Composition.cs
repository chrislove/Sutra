using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial struct SharpFunc<TOut> {
		/// <summary>
		/// Function composition operator
		/// </summary>
		public static SharpFunc<TOut> operator +( SharpFunc<TOut> lhs, [NotNull] SharpFunc rhs )
												=> lhs + (i => rhs.Func(i));
		
		/// <summary>
		/// Function composition operator
		/// </summary>
		public static SharpFunc<TOut> operator +( SharpFunc<TOut> lhs, SharpFunc<TOut> rhs )
												=> lhs + (i => rhs.Func(i));
		
		/// <summary>
		/// Function composition operator
		/// </summary>
		public static SharpFunc<TOut> operator +( SharpFunc<TOut> lhs, [NotNull] Func<object, object> rhs ) {
			if (rhs == null) throw new ArgumentNullException(nameof(lhs));

			return SharpFunc.FromFunc(
			                        i => rhs(lhs.Func(i)).To<TOut>($"{lhs.T()} + {rhs.T()}")
			                       );
		}

		/*
		/// <summary>
		/// Function composition operator
		/// </summary>
		public static SharpAct<TOut> operator +( SharpFunc<TOut> lhs, [NotNull] Action<TOut> rhs ) {
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			return SharpAct.FromAction<TOut>(
			                                 i => rhs(lhs.Func(i))
			                                );
		}*/

		/*
		/// <summary>
		/// Function composition operator
		/// </summary>
		public static SharpAct<TOut> operator +( SharpFunc<TOut> lhs, SharpAct<TOut> rhs ) => lhs + rhs.Action;*/
	}
}