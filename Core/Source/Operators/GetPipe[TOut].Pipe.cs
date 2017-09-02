using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial class GetPipe<TOut> {
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static SharpAct operator |( GetPipe<TOut> lhs, Action<TOut> rhs ) {
			// Type validation not needed

			var combined = lhs.Func + rhs;

			return SharpAct.FromAction(combined);
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static TOut operator |( GetPipe<TOut> lhs, PipeEnd pipeEnd ) {
			return lhs.Get;
		}

		[NotNull]
		public static SharpAct operator |( GetPipe<TOut> lhs, SharpAct<object> rhs ) {
			// Type validation not needed

			return lhs | (p => rhs.Action(p));
		}

		[NotNull]
		public static SharpAct operator |( GetPipe<TOut> lhs, SharpAct<TOut> rhs ) {
			// Type validation not needed

			return lhs | (p => rhs.Action(p));
		}
	}
}