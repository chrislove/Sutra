using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	internal static class PipeFunc {
		internal static PipeFunc<TIn, TOut> FromFunc<TIn, TOut>( [NotNull] Func<TIn, TOut> func )
																		=> new PipeFunc<TIn, TOut>(func);
	}
}