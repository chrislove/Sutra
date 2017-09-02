using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial class GetPipe<TOut> : GetPipe
	{
		[NotNull] internal new SharpFunc<TOut> Func { get; }

		internal GetPipe([NotNull] ISharpFunc func) : base(func) {
			Func = (func ?? throw new ArgumentNullException(nameof(func))).ToOut<TOut>();
		}

		internal TOut Get => Func.Func(default);
	}
}