using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	internal static class SharpFunc {
		internal static SharpFunc<TIn, TOut> FromFunc<TIn, TOut>( [NotNull] Func<TIn, TOut> func )
																		=> new SharpFunc<TIn, TOut>(func);

		internal static SharpFunc<TOut> FromFunc<TOut>([NotNull] Func<object, TOut> func) => new SharpFunc<TOut>(func);
		internal static SharpFunc<TOut> FromFunc<TOut>([NotNull] Func<TOut> func) => new SharpFunc<TOut>( func );
		internal static SharpFunc<TOut> FromFunc<TOut>(SharpFunc<TOut> sharpFunc)
													=> new SharpFunc<TOut>(i => sharpFunc.Func(i).To<TOut>(nameof(SharpFunc.FromFunc)));

		/// <summary>
		/// Creates a SharpFunc that contains the input value.
		/// </summary>
		/// <param name="obj">Value to wrap</param>
		public static SharpFunc<TOut> WithValue<TOut>(TOut obj) => SharpFunc.FromFunc(i => obj);
	}
}