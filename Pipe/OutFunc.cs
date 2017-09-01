using System;
using JetBrains.Annotations;

namespace SharpPipe {
	internal static class OutFunc {
		[NotNull] internal static OutFunc<TOut> FromFunc<TOut>([NotNull] Func<object, TOut> func) => new OutFunc<TOut>(func);
		[NotNull] internal static OutFunc<TOut> FromFunc<TOut>([NotNull] SharpFunc sharpFunc)
													=> new OutFunc<TOut>(i => sharpFunc.Func(i).To<TOut>());

		/// <summary>
		/// Creates a SharpFunc that contains the input value.
		/// </summary>
		/// <param name="obj">Value to wrap</param>
		[NotNull]
		public static OutFunc<TOut> WithValue<TOut>(TOut obj) => OutFunc.FromFunc(i => obj);
	}
}