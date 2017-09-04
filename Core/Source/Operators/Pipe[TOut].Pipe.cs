using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial struct Pipe<TOut> {
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static SharpAct operator |( Pipe<TOut> lhs, Action<TOut> rhs ) {
			var combined = lhs.Func + rhs;

			return SharpAct.FromAction(combined);
		}
		
		public static Pipe<TOut>    operator |( Pipe<TOut> lhs, Func<TOut, TOut> rhs ) => PIPE.IN( lhs.Func + SharpFunc.FromFunc(rhs) );
		
		public static ActPipe<TOut> operator |( Pipe<TOut> lhs, ToActPipe rhs )        => ActPipe.FromPipe(lhs);

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static Pipe<TOut> operator |( Pipe<TOut> lhs, SharpFunc<TOut> rhs ) {
			return PIPE.IN(lhs.Func + rhs);
		}
		
		/// <summary>
		/// Replaces pipe contents with object
		/// </summary>
		public static Pipe<TOut> operator |( Pipe<TOut> lhs, TOut rhs ) => PIPE.IN(rhs);
	}
}