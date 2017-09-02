using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed partial class SharpFunc<TOut> : SharpFunc<object, TOut> {
		[NotNull] internal new Func<object, TOut> Func => ~this;

		internal SharpFunc([NotNull] Func<object, TOut> func) : base(func) { }
	}
}