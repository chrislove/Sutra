using JetBrains.Annotations;
using System;
using System.Linq;
using static SharpPipe.Commands;

namespace SharpPipe
{
	public static partial class Commands {
		public static partial class func {
			public partial class takes<TIn> {
				public static PipeFunc<TIn, TOut> from<TOut>( [NotNull] Func<TIn, TOut> func ) => new PipeFunc<TIn, TOut>(func);
			}
		}
	}

	/// <summary>
	/// A function transforming a single value.
	/// </summary>
	public partial struct PipeFunc<TIn, TOut> {
		[NotNull] private Func<TIn, TOut> Func { get; }

		internal PipeFunc([NotNull] Func<TIn, TOut> func) => Func = func ?? throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Use this operator to invoke the function.
		/// </summary>
		public TOut this[ [CanBeNull] TIn invalue ] => Func(invalue);

		[NotNull]
		public static implicit operator Func<TIn, TOut>( PipeFunc<TIn, TOut> pipeFunc ) => pipeFunc.Func;
		public static implicit operator PipeFunc<TIn, TOut>( [NotNull] Func<TIn, TOut> func ) => Commands.func.takes<TIn>.from(func);
		
		
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static Pipe<TOut> operator |( Pipe<TIn> pipe, PipeFunc<TIn, TOut> func ) => start<TOut>.pipe | func[pipe.Get];
		
		/// <summary>
		/// Forward pipe operator. Transforms an sequence.
		/// </summary>
		[UsedImplicitly]
		public static Seq<TOut> operator |( Seq<TIn> pipe, PipeFunc<TIn, TOut> func ) {
			var enumerable = pipe.Get.Select(i => func.Func(i).To<TOut>($"{pipe.T()} | {func.T()}"));

			return enumerable | to<TOut>.pipe;
		}
	}
}