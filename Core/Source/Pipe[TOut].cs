using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial struct Pipe<TOut> : IPipe<TOut>
	{
		internal SharpFunc<TOut> Func { get; }
		[CanBeNull] internal TOut Get => Func.Func(null);

		internal Pipe( SharpFunc<TOut> func )      => Func = SharpFunc.FromFunc(func);
		internal Pipe( [NotNull] Func<TOut> func ) => Func = SharpFunc.FromFunc(func);
	}
}