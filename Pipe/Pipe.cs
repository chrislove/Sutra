using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public static class Pipe {
		/// <summary>
		/// Marks the beginning of a pipe.
		/// </summary>
		[NotNull] public static EmptyPipe PIPE => new EmptyPipe();

		/// <summary>
		/// Signals a pipe to return its value.
		/// </summary>
		[NotNull] public static PipeEnd END => new PipeEnd();

		/// <summary>
		/// Signals a pipe to return its value.
		/// </summary>
		[NotNull] public static PipeEnd __ => END;

		/// <summary>
		/// Initializes pipe with an object.
		/// </summary>
		[NotNull] public static GetPipe<TIn> IN<TIn>( [NotNull] TIn obj) => new GetPipe<TIn>(obj);

		/// <summary>
		/// Executes a pipe.
		/// </summary>
		/// <example>
		/// <code>
		///    _( PIPE | DateTime.Now | Print );
		/// </code>
		/// </example>
		public static void _( [NotNull] ActPipe pipe) => pipe.Do();

		/*
		/// <summary>
		/// Returns pipe contents
		/// </summary>
		/// <typeparam name="TOut"></typeparam>
		/// <param name="pipeResult"></param>
		/// <returns></returns>
		[NotNull]
		public static TOut _<TOut>( [NotNull] TOut pipeResult ) => pipeResult;
		*/

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

		//[NotNull] public static GetPipe<TOut> _<TOut>([CanBeNull] Func<dynamic, TOut> func) => new GetPipe<TOut>(func);

		[NotNull] public static ActPipe _<TIn>([CanBeNull] Action<TIn> act) => new ActPipe(i =>act(i.To<TIn>()));

		//public static ActPipe _([NotNull] Action<dynamic> pipe) => pipe.Do();


		/*
		/// <summary>
		/// Creates a pipe-compatible function.
		/// </summary>
		/// <example>
		/// <code>
		///    _( p => DoStuff(p) )
		/// </code>
		/// </example>
		/// <param name="func">Function to convert</param>
		[NotNull] public static F _( [CanBeNull] Func<dynamic, object> func) => p => func(p);*/

			/*
		/// <summary>
		/// Creates a strongly-typed pipe-compatible action.
		/// </summary>
		/// <example>
		/// <code>
		///    __{string}(Console.WriteLine)
		/// </code>
		/// </example>
		/// <typeparam name="TIn">Action parameter type.</typeparam>
		/// <param name="act">Action to convert</param>
		[NotNull] public static A __<TIn>( [CanBeNull] Action<TIn> act) => p => act(p.To<TIn>());

		/// <summary>
		/// Creates a pipe-compatible action.
		/// </summary>
		/// <example>
		/// <code>
		///    __(p => Console.WriteLine()p)
		/// </code>
		/// </example>
		/// <param name="act">Action to convert</param>
		[NotNull] public static A __( [CanBeNull] Action<dynamic> act) => p => act(p);*/

	}
}