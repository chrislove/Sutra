using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial class Pipe<TOut> {
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static SharpAct operator |( Pipe<TOut> lhs, Action<TOut> rhs ) {
			var combined = lhs.Func + rhs;

			return SharpAct.FromAction(combined);
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static TOut operator |( Pipe<TOut> lhs, PipeEnd pipeEnd ) {
			return lhs.Get;
		}

		[NotNull]
		public static SharpAct operator |( Pipe<TOut> lhs, SharpAct<object> rhs ) {
			return lhs | (p => rhs.Action(p));
		}

		[NotNull]
		public static SharpAct operator |( Pipe<TOut> lhs, SharpAct<TOut> rhs ) {
			return lhs | (p => rhs.Action(p));
		}
	}
}