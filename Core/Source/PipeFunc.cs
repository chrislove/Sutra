using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	internal static class PipeFunc {
		internal static PipeFunc<TIn, TOut> FromFunc<TIn, TOut>( [NotNull] Func<TIn, TOut> func )
																		=> new PipeFunc<TIn, TOut>(func);

		internal static PipeFunc<TOut> FromFunc<TOut>([NotNull] Func<object, TOut> func) => new PipeFunc<TOut>(func);
		internal static PipeFunc<TOut> FromFunc<TOut>([NotNull] Func<TOut> func) => new PipeFunc<TOut>( func );
		internal static PipeFunc<TOut> FromFunc<TOut>(PipeFunc<TOut> pipeFunc)
													=> new PipeFunc<TOut>(i => pipeFunc.Func(i).To<TOut>(nameof(PipeFunc.FromFunc)));
	}
}