using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public static partial class Pipe
	{
		internal static Pipe<T> FromObject<T>(T obj)          => FromFunc(SharpFunc.WithValue(obj));
		internal static Pipe<T> FromFunc<T>(IOutFunc<T> func) => new Pipe<T>(func);

		/// <summary>
		/// Creates and initializes Pipe{T} with an object.
		/// </summary>
		public static Pipe<TOut> IN<TOut>([NotNull] TOut obj) => Pipe.FromObject(obj);

		/// <summary>
		/// Creates a strongly-typed pipe-compatible function.
		/// </summary>
		/// <example>
		/// <code>
		///    _{DateTime}( p => GetDate(p) )
		/// </code>
		/// </example>
		public static SharpFunc<TOut> _<TOut>([CanBeNull] Func<object, TOut> func) => SharpFunc.FromFunc(func);
		
		/// <summary>
		/// Same input and output type
		/// </summary>
		public static SharpFunc<T, T> _<T>([CanBeNull] Func<T, T> func)            => SharpFunc.FromFunc(func);
		
		/// <summary>
		/// Creates a strongly-typed pipe-compatible function.
		/// </summary>
		/// <example>
		/// <code>
		///    _{DateTime, string}( p => GetDate(p) )
		/// </code>
		/// </example>
		public static SharpFunc<TIn, TOut> _<TIn, TOut>([CanBeNull] Func<TIn, TOut> func) => SharpFunc.FromFunc(func);
	}
}