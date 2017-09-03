using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	public abstract partial class SharpFunc : ISharpFunc {
		public Func<object, object> Func { get; }

		protected SharpFunc([NotNull] Func<object, object> func) {
			Func = func ?? throw new ArgumentNullException(nameof(func));
		}

		internal static SharpFunc<TIn, TOut> FromFunc<TIn, TOut>( [NotNull] Func<TIn, TOut> func )
																		=> new SharpFunc<TIn, TOut>(func);

		internal static SharpFunc<TOut> FromFunc<TOut>([NotNull] Func<object, TOut> func) => new SharpFunc<TOut>(func);
		internal static SharpFunc<TOut> FromFunc<TOut>([NotNull] Func<TOut> func) => new SharpFunc<TOut>( func );
		internal static SharpFunc<TOut> FromFunc<TOut>([NotNull] ISharpFunc sharpFunc)
													=> new SharpFunc<TOut>(i => sharpFunc.Func(i).To<TOut>());

		/// <summary>
		/// Creates a SharpFunc that contains the input value.
		/// </summary>
		/// <param name="obj">Value to wrap</param>
		public static SharpFunc<TOut> WithValue<TOut>(TOut obj) => SharpFunc.FromFunc(i => obj);
	}
}