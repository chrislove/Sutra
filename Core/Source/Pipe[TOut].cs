using System;
using JetBrains.Annotations;
using static SharpPipe.Pipe;

namespace SharpPipe {
	public partial struct Pipe<TOut> : IPipe
	{
		internal SharpFunc<TOut> Func { get; }
		SharpFunc<object> IPipe.Func => SharpFunc.FromFunc<object>(Func);

		internal Pipe([NotNull] ISharpFunc func) {
			Func = (func ?? throw new ArgumentNullException(nameof(func))).ToOut<TOut>();
		}

		[CanBeNull] internal TOut Get => Func.Func(default(TOut));

		/// <summary>
		/// Pipe decomposition operator.
		/// Returns the value contained within the pipe. An equivalent of: pipe | OUT
		/// </summary>
		[NotNull]
		public static TOut operator ~( Pipe<TOut> pipe ) => pipe | OUT;
	}
}