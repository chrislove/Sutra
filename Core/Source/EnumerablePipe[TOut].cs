using JetBrains.Annotations;
using System.Collections.Generic;

namespace SharpPipe
{
	public sealed partial class EnumerablePipe<TOut> : GetPipe<IEnumerable<TOut>> {
		internal EnumerablePipe([CanBeNull] IEnumerable<TOut> obj) : this(SharpFunc.WithValue(obj) ) { }

		internal EnumerablePipe([NotNull] IOutFunc<IEnumerable<TOut>> func) : base(func) {}
	}
}