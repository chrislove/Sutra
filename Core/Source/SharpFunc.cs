using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	public abstract partial class SharpFunc : ISharpFunc {
		[NotNull] public Func<object, object> Func { get; }

		protected SharpFunc([NotNull] Func<object, object> func) {
			Func = func ?? throw new ArgumentNullException(nameof(func));
		}

		/// <summary>
		/// Returns wrapped function.
		/// </summary>
		[NotNull]
		public static Func<object, object> operator ~(SharpFunc sharpFunc) => sharpFunc.Func;

		[NotNull]
		internal static SharpFunc<TIn, TOut> FromFunc<TIn, TOut>( [NotNull] Func<TIn, TOut> func )
																		=> new SharpFunc<TIn, TOut>(func);

		[NotNull] internal static SharpFunc<TOut> FromFunc<TOut>([NotNull] Func<object, TOut> func) => new SharpFunc<TOut>(func);
		[NotNull]
		internal static SharpFunc<TOut> FromFunc<TOut>([NotNull] SharpFunc sharpFunc)
													=> new SharpFunc<TOut>(i => sharpFunc.Func(i).To<TOut>());

		/// <summary>
		/// Creates a SharpFunc that contains the input value.
		/// </summary>
		/// <param name="obj">Value to wrap</param>
		[NotNull]
		public static SharpFunc<TOut> WithValue<TOut>(TOut obj) => SharpFunc.FromFunc(i => obj);
	}
}