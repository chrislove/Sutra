using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed class GetPipe<TOut> : GetPipe {
		internal GetPipe( TOut obj ) : this(_ => obj) {}

		internal GetPipe( [NotNull] Func<object, TOut> func ) : base(true) {
			if (func == null) throw new ArgumentNullException(nameof(func));
			Func = i => func(i);
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static ActPipe operator |(GetPipe<TOut> lhs, ActPipe rhs)
		{
			return lhs | (p => rhs.Act(p) );
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static ActPipe operator |( GetPipe<TOut> lhs, A rhs ) {
			Action<object> combined = i => rhs(lhs.Func(i));
			return new ActPipe(i => combined(i));
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static TOut operator |( GetPipe<TOut> lhs, PipeEnd pipeEnd ) {
			return lhs.Get<TOut>();
		}
	}
}