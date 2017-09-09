using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	public static partial class Commands {
		public static partial class FUNC<TIn> {
			public static PipeFunc<TIn, TOut> New<TOut>( Func<TIn, TOut> func ) => new PipeFunc<TIn, TOut>(func);
		}
	}

	public partial struct PipeFunc<TIn, TOut> {
		[NotNull] private Func<TIn, TOut> Func { get; }

		internal PipeFunc([NotNull] Func<TIn, TOut> func) => Func = func ?? throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Use this operator to invoke the function.
		/// </summary>
		public TOut this[ [CanBeNull] TIn invalue ] => Func(invalue);

		[NotNull]
		public static implicit operator Func<TIn, TOut>( PipeFunc<TIn, TOut> pipeFunc )
																	=> pipeFunc.Func;

	}
}