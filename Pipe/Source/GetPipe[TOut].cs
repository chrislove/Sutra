using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public class GetPipe<TOut> : GetPipe//, IGetPipe
	{
		[NotNull] internal new OutFunc<TOut> Func { get; }
		//[NotNull] OutFunc<object> IGetPipe.Func => Func.ToOut<object>();

		internal GetPipe([NotNull] ISharpFunc func ) : base(func) {
			//Unable to cast object of type SharpFunc<DateTime, DateTime> to type OutFunc<DateTime>

			Func = (func ?? throw new ArgumentNullException(nameof(func))).ToOut<TOut>();
		}

		internal TOut Get => Func.Func(default);


		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static ActPipe<TOut> operator |(GetPipe<TOut> lhs, Action<TOut> rhs)
		{
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