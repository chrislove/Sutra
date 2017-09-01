using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe
{
	public static class Pipe
	{
		/// <summary>
		/// Signals a pipe to return its value.
		/// </summary>
		[NotNull] public static PipeEnd ___ => new PipeEnd();

		/// <summary>
		/// Initializes pipe with an object.
		/// </summary>
		[NotNull] public static GetPipe<TOut> IN<TOut>([NotNull] TOut obj) => GetPipe.FromObject(obj);

		/// <summary>
		/// Initializes pipe with an object.
		/// </summary>
		[NotNull] public static EnumerablePipe<TOut> IN<TOut>([NotNull] IEnumerable<TOut> obj) => EnumerablePipe.FromEnumerable(obj);


		/// <summary>
		/// Executes a pipe.
		/// </summary>
		/// <example>
		/// <code>
		///    _( PIPE | DateTime.Now | Print );
		/// </code>
		/// </example>
		public static void _<TIn>([NotNull] ActPipe<TIn> pipe) => pipe.Do();

		/// <summary>
		/// Creates a strongly-typed pipe-compatible function.
		/// </summary>
		/// <example>
		/// <code>
		///    _{DateTime}( p => GetDate(p) )
		/// </code>
		/// </example>
		[NotNull] public static OutFunc<TOut> _<TOut>([CanBeNull] Func<object, TOut> func) => OutFunc.FromFunc(func);

		/// <summary>
		/// Creates a strongly-typed pipe-compatible function.
		/// </summary>
		/// <example>
		/// <code>
		///    _{DateTime, string}( p => GetDate(p) )
		/// </code>
		/// </example>
		//[NotNull] public static OutFunc<TOut> _<TIn, TOut>([CanBeNull] Func<TIn, TOut> func) => OutFunc.FromFunc(i => func(i.To<TIn>()));

		[NotNull] public static SharpFunc<TIn, TOut> _<TIn, TOut>([CanBeNull] Func<TIn, TOut> func) => SharpFunc.FromFunc(func);

		[NotNull] public static SharpAct<TIn> __<TIn>([CanBeNull] Action<TIn> act) => SharpAct.FromAction<TIn>(i => act(i.To<TIn>()));
	}
}