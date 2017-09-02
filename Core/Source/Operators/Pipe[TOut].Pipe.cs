using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial struct Pipe<TOut> {
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static SharpAct operator |( Pipe<TOut> lhs, Action<TOut> rhs ) {
			var combined = lhs.Func + rhs;

			return SharpAct.FromAction(combined);
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