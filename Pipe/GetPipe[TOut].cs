using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public class GetPipe<TOut> : PipeBase
	{
		[NotNull] internal OutFunc<TOut> Func { get; }

		internal GetPipe([NotNull] ISharpFunc func ) {
			//Unable to cast object of type SharpFunc<DateTime, DateTime> to type OutFunc<DateTime>

			Func = (func ?? throw new ArgumentNullException(nameof(func))).ToOut<TOut>();
		}

		internal TOut Get => Func.Func(null);


		/*
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static ActPipe<TOut> operator |(GetPipe<TOut> lhs, ActPipe<TOut> rhs)
		{
			return lhs | (p => rhs.Act(p) );
		}*/

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