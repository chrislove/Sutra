using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	public partial class SharpFunc<TIn, TOut> : SharpFunc, IOutFunc<TOut> {
		[NotNull] internal new Func<TIn, TOut> Func           => base.Func.To<TIn, TOut>();
		[NotNull] Func<object, TOut> IOutFunc<TOut>.Func      => base.Func.ToOut<TOut>();

		internal SharpFunc( [NotNull] Func<TIn, TOut> func, Type inType = null, Type outType = null ) :
										base(i => func(i.To<TIn>()), inType, outType) { }
	}
}