using System;

namespace SharpPipe {
	public partial struct Pipe<TOut> {
		public static Pipe<TOut> operator |( Pipe<TOut> lhs, Func<TOut, TOut> rhs ) => PIPE.IN( lhs.Func + SharpFunc.FromFunc(rhs) );

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static Pipe<TOut> operator |( Pipe<TOut> lhs, SharpFunc<TOut> rhs ) => PIPE.IN(lhs.Func + rhs);

		/// <summary>
		/// Replaces pipe contents with object
		/// </summary>
		public static Pipe<TOut> operator |( Pipe<TOut> lhs, TOut rhs ) => PIPE.IN(rhs);
	}
}