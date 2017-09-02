using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial class Pipe<TOut> : IPipe
	{
		[NotNull] internal SharpFunc<TOut> Func { get; }
		SharpFunc<object> IPipe.Func => SharpFunc.FromFunc<object>(Func);


		internal Pipe([NotNull] ISharpFunc func) {
			Func = (func ?? throw new ArgumentNullException(nameof(func))).ToOut<TOut>();
		}

		internal TOut Get => Func.Func(default(TOut));
	}
}