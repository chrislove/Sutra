using System;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
	public partial struct Pipe<TOut>
	{
		internal SharpFunc<TOut> Func { get; }

		internal Pipe( SharpFunc<TOut> func ) => Func = SharpFunc.FromFunc(func);
		internal Pipe( [NotNull] Func<TOut> func ) => Func = SharpFunc.FromFunc(func);

		[CanBeNull] private TOut Get => Func.Func(null);

		/// <summary>
		/// Pipe decomposition operator.
		/// Returns the value contained within the pipe. An equivalent of: pipe | OUT
		/// </summary>
		[NotNull]
		public static TOut operator ~( Pipe<TOut> pipe ) => pipe | OUT;
	}
}