using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public static class FuncExtensions {
		[NotNull] public static SharpFunc Sharp(this F func) => new SharpFunc(func);
		[NotNull] public static SharpFunc<TIn, TOut> Sharp<TIn, TOut>(this Func<TIn, TOut> func) => new SharpFunc<TIn, TOut>(func);
		[NotNull] public static SharpFunc<TIn> Sharp<TIn>(this Func<TIn, object> func) => new SharpFunc<TIn>(func);
	}
}