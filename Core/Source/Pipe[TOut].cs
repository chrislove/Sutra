using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace SharpPipe {
	[EditorBrowsable(EditorBrowsableState.Never)]
	public partial struct Pipe<TOut> : IPipe<TOut>
	{
		internal SharpFunc<TOut> Func { get; }
		[CanBeNull] internal TOut Get => Func.Func(null);

		internal Pipe( [CanBeNull] TOut obj ) : this( SharpFunc.WithValue(obj) ) {}
		internal Pipe( SharpFunc<TOut> func )      => Func = SharpFunc.FromFunc(func);
		internal Pipe( [NotNull] Func<TOut> func ) => Func = SharpFunc.FromFunc(func);

	}
}