using JetBrains.Annotations;
using System;
using System.Linq;
using static SharpPipe.Commands;

namespace SharpPipe
{
	public static partial class func{
		public partial class takes<TIn> {
			public static func<TIn, TOut> from<TOut>( [NotNull] Func<TIn, TOut> func ) => new func<TIn, TOut>(func);
		}
	}

	/// <summary>
	/// A function transforming a single value.
	/// </summary>
	public partial struct func<TIn, TOut> {
		[NotNull] private Func<TIn, TOut> Func { get; }

		internal func([NotNull] Func<TIn, TOut> func) => Func = func ?? throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Use this operator to invoke the function.
		/// </summary>
		public TOut this[ [CanBeNull] TIn invalue ] => Func(invalue);

		[NotNull]
		public static implicit operator Func<TIn, TOut>( func<TIn, TOut> func ) => func.Func;
		public static implicit operator func<TIn, TOut>( [NotNull] Func<TIn, TOut> func ) => SharpPipe.func.takes<TIn>.from(func);
		
		
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static Pipe<TOut> operator |( Pipe<TIn> pipe, func<TIn, TOut> rhs ) => new Pipe<TOut>(rhs[pipe.get]);
		
		/// <summary>
		/// Forward pipe operator. Transforms an Sequence.
		/// </summary>
		[UsedImplicitly]
		public static Seq<TOut> operator |( Seq<TIn> pipe, func<TIn, TOut> func ) {
			var enumerable = pipe.get.Select(i => func.Func(i).To<TOut>($"{pipe.T()} | {func.T()}"));

			return enumerable | to<TOut>.pipe;
		}
	}
}