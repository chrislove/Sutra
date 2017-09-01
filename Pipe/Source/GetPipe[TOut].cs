using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public class GetPipe<TOut> : GetPipe//, IGetPipe
	{
		[NotNull] internal new OutFunc<TOut> Func { get; }

		internal GetPipe([NotNull] ISharpFunc func) : base(func, typeof(TOut)) {
			Func = (func ?? throw new ArgumentNullException(nameof(func))).ToOut<TOut>();
		}

		internal TOut Get => Func.Func(default);


		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static ActPipe<TOut> operator |(GetPipe<TOut> lhs, Action<TOut> rhs)
		{
			// Type validation not needed

			var combined = lhs.Func + rhs;

			return ActPipe.FromAction(combined);
		}


		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static TOut operator |( GetPipe<TOut> lhs, PipeEnd pipeEnd ) {
			return lhs.Get;
		}
	}
}