using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	public partial struct SharpFunc<TIn, TOut> {
		[NotNull] internal Func<TIn, TOut> Func { get; }

		internal SharpFunc([NotNull] Func<TIn, TOut> func) {
			Func = func ?? throw new ArgumentNullException(nameof(func));
		}
	}
}