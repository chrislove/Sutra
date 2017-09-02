using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	public partial class SharpFunc<TIn, TOut> : SharpFunc, IOutFunc<TOut> {
		[NotNull] private new Func<TIn, TOut> Func            => base.Func.To<TIn, TOut>();
		[NotNull] Func<object, TOut> IOutFunc<TOut>.Func      => base.Func.ToOut<TOut>();

		/// <summary>
		/// Returns wrapped function.
		/// </summary>
		[NotNull]
		public static Func<TIn, TOut> operator ~(SharpFunc<TIn, TOut> sharpFunc) => sharpFunc.Func;


		internal SharpFunc( [NotNull] Func<TIn, TOut> func ) :
										base(i => func(i.To<TIn>())) { }
	}
}