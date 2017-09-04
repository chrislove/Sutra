using System;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe
{
	public static partial class Pipe
	{
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
		/// Same input and output type.
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