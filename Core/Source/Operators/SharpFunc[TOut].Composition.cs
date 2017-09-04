using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial struct SharpFunc<TOut> {
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static IOutFunc<TOut> operator +( SharpFunc<TOut> lhs, [NotNull] ISharpFunc rhs )
												=> lhs + (i => rhs.Func(i));
		
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static IOutFunc<TOut> operator +( SharpFunc<TOut> lhs, [NotNull] IOutFunc<TOut> rhs )
												=> lhs + (i => rhs.Func(i));
		
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static IOutFunc<TOut> operator +( SharpFunc<TOut> lhs, [NotNull] Func<object, object> rhs ) {
			if (rhs == null) throw new ArgumentNullException(nameof(lhs));

			return SharpFunc.FromFunc(
			                        i => rhs(lhs.Func(i))
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