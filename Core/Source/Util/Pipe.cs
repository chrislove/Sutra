using System;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe
{
	public static class Pipe
	{
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