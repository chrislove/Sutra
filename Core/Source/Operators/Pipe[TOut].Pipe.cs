using System;

namespace SharpPipe {
	public partial struct Pipe<TOut> {
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static SharpAct operator |( Pipe<TOut> lhs, Action<TOut> rhs ) {
			var combined = lhs.Func + rhs;

			return SharpAct.FromAction(combined);
		}

		public static SharpAct operator |( Pipe<TOut> lhs, SharpAct<object> rhs ) {
			return lhs | (p => rhs.Action(p));
		}

		public static SharpAct operator |( Pipe<TOut> lhs, SharpAct<TOut> rhs ) {
			return lhs | (p => rhs.Action(p));
		}
	}
}