using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed partial class SharpFunc<TOut> : SharpFunc<object, TOut> {
		[NotNull] internal new Func<object, TOut> Func => ~this;

		internal SharpFunc([NotNull] Func<object, TOut> func) : this(func, typeof(TOut)) { }

		private SharpFunc( [NotNull] Func<object, TOut> func, Type outType ) : base(func, null, outType) {}
	}
}