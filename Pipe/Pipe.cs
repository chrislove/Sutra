using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public static class Pipe {
		/// <summary>
		/// Signals a pipe to return its value.
		/// </summary>
		[NotNull] public static PipeEnd __ => new PipeEnd();

		/// <summary>
		/// Initializes pipe with an object.
		/// </summary>
		[NotNull] public static GetPipe<TOut> IN<TOut>([NotNull] TOut obj) => new GetPipe<TOut>(obj);

		/// <summary>
		/// Executes a pipe.
		/// </summary>
		/// <example>
		/// <code>
		///    _( PIPE | DateTime.Now | Print );
		/// </code>
		/// </example>
		public static void _( [NotNull] ActPipe pipe) => pipe.Do();

		/// <summary>
		/// Creates a strongly-typed pipe-compatible function.
		/// </summary>
		/// <example>
		/// <code>
		///    _{DateTime}( p => GetDate(p) )
		/// </code>
		/// </example>
		/// <typeparam name="TIn">Function parameter type.</typeparam>
		/// <param name="func">Function to convert</param>
		[NotNull] public static SharpFunc<TIn, TOut> _<TIn, TOut>([CanBeNull] Func<TIn, TOut> func) => new SharpFunc<TIn, TOut>(func);


		[NotNull] public static ActPipe _<TIn>([CanBeNull] Action<TIn> act) => new ActPipe(i =>act(i.To<TIn>()));
	}
}