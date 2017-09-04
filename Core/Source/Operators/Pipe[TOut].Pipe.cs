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
		
		public static Pipe<TOut>    operator |( Pipe<TOut> lhs, Func<TOut, TOut> rhs ) => Pipe.FromFunc(lhs.Func + SharpFunc.FromFunc(rhs) );
		
		public static ActPipe<TOut> operator |( Pipe<TOut> lhs, ToActPipe rhs )        => ActPipe.FromPipe(lhs);
	}
}