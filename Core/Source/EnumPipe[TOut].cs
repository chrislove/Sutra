using System;
using JetBrains.Annotations;
using System.Collections.Generic;
using static SharpPipe.Commands;

namespace SharpPipe
{
	public partial struct EnumPipe<TOut> : IPipe<TOut> {
		internal EnumPipe([CanBeNull] IEnumerable<TOut> obj) : this(SharpFunc.WithValue(obj) ) { }

		private EnumPipe( SharpFunc<IEnumerable<TOut>> func ) => Func = func;

		private SharpFunc<IEnumerable<TOut>> Func { get; }

		[NotNull] internal IEnumerable<TOut> Get => Func.NotNullFunc(default(IEnumerable<TOut>)).NotNull(typeof(EnumPipe<TOut>));
	}
}