using System;

namespace SharpPipe {
	public partial struct Pipe<TOut> {
		public static Pipe<TOut> operator |( Pipe<TOut> pipe, Func<TOut, TOut> rhs ) => new Pipe<TOut>( rhs(pipe.Get) );

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static Pipe<TOut> operator |( Pipe<TOut> pipe, SharpFunc<TOut> rhs ) => new Pipe<TOut>(pipe.Func + rhs);

		/// <summary>
		/// Replaces pipe contents with object
		/// </summary>
		public static Pipe<TOut> operator |( Pipe<TOut> pipe, TOut rhs ) => new Pipe<TOut>(rhs);
	}
}