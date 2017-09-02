using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe
{
	public static partial class Pipe
	{
		[NotNull] internal static Pipe<T> FromObject<T>(T obj) => FromFunc(SharpFunc.WithValue(obj));
		[NotNull] internal static Pipe<T> FromFunc<T>(IOutFunc<T> func) => new Pipe<T>(func);

		/// <summary>
		/// Signals a pipe to return its value.
		/// </summary>
		[NotNull] public static PipeEnd __ => new PipeEnd();

		/// <summary>
		/// Initializes pipe with an object.
		/// </summary>
		[NotNull] public static Pipe<TOut> IN<TOut>([NotNull] TOut obj) => Pipe.FromObject(obj);

		/// <summary>
		/// Initializes pipe with an object.
		/// </summary>
		[NotNull] public static EnumerablePipe<TOut> IN<TOut>([NotNull] IEnumerable<TOut> obj) => EnumerablePipe.FromEnumerable(obj);


		/// <summary>
		/// Executes a SharpAct.
		/// </summary>
		/// <example>
		/// <code>
		///    _( PIPE | DateTime.Now | Print );
		/// </code>
		/// </example>
		public static void _( [NotNull] SharpAct act ) => act.Action(default);

		/// <summary>
		/// Creates a strongly-typed pipe-compatible function.
		/// </summary>
		/// <example>
		/// <code>
		///    _{DateTime}( p => GetDate(p) )
		/// </code>
		/// </example>
		[NotNull] public static SharpFunc<TOut> _<TOut>([CanBeNull] Func<object, TOut> func) => SharpFunc.FromFunc(func);


		/// <summary>
		/// Creates a strongly-typed pipe-compatible function.
		/// </summary>
		/// <example>
		/// <code>
		///    _{DateTime, string}( p => GetDate(p) )
		/// </code>
		/// </example>
		[NotNull] public static SharpFunc<TIn, TOut> _<TIn, TOut>([CanBeNull] Func<TIn, TOut> func) => SharpFunc.FromFunc(func);

		[NotNull] public static SharpAct<TIn> _<TIn>([CanBeNull] Action<TIn> act) => SharpAct.FromAction<TIn>(i => act(i.To<TIn>()));
	}
}