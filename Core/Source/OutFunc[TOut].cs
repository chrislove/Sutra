using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed partial class OutFunc<TOut> : SharpFunc<object, TOut> {
		[NotNull] internal new Func<object, TOut> Func => base.Func;

		internal OutFunc([NotNull] Func<object, TOut> func) : this(func, typeof(TOut)) { }

		private OutFunc( [NotNull] Func<object, TOut> func, Type outType ) : base(func, null, outType) {}
	}
}