using System;
using JetBrains.Annotations;

namespace SharpPipe {
	internal static class InFunc {
		[NotNull] internal static InFunc<TIn> FromFunc<TIn>([NotNull] Func<TIn, object> func) => new InFunc<TIn>(func);
		[NotNull] internal static InFunc<TIn> FromFunc<TIn>([NotNull] SharpFunc sharpFunc)
			=> new InFunc<TIn>(i => sharpFunc.Func(i));
	}
}