using System;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe
{
	public static partial class Commands {
		/// <summary>
		/// Pipe factory.
		/// </summary>
		public static partial class func {
			/// <summary>
			/// Specifies the input value of a function.
			/// </summary>
			public partial class takes<TIn> {
				/// <summary>
				/// Creates pipe function from a system function.
				/// </summary>
				public static PipeFunc<TIn, TOut> from<TOut>( [NotNull] Func<TIn, TOut> func ) => new PipeFunc<TIn, TOut>(func);
			}
		}
	}

	/// <summary>
	/// Function transforming pipe or sequence value.
	/// </summary>
	public partial struct PipeFunc<TIn, TOut> {
		/// <summary>
		/// Inner function
		/// </summary>
		[PublicAPI]
		[NotNull] public Func<TIn, TOut> Func { get; }

		internal PipeFunc([NotNull] Func<TIn, TOut> func) => Func = func ?? throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Use this operator to invoke the function.
		/// </summary>
		public TOut this[ [CanBeNull] TIn invalue ] => Func(invalue);

		[NotNull]
		public static implicit operator Func<TIn, TOut>( PipeFunc<TIn, TOut> pipeFunc ) => pipeFunc.Func;
		public static implicit operator PipeFunc<TIn, TOut>( [NotNull] Func<TIn, TOut> func ) => Commands.func.takes<TIn>.from(func);
		
		
		/// <summary>
		/// Transforms a pipe.
		/// </summary>
		public static Pipe<TOut> operator |( Pipe<TIn> pipe, PipeFunc<TIn, TOut> func ) {
			foreach (var value in pipe.Option)
				return start<TOut>.pipe | func[value];

			return Pipe<TOut>.SkipPipe;
		}

		/// <summary>
		/// Transforms a sequence.
		/// </summary>
		public static Seq<TOut> operator |( Seq<TIn> seq, PipeFunc<TIn, TOut> func ) {
			foreach (var value in seq.Option) {
				TOut Selector( TIn i ) => func.Func(i).To<TOut>(seq, func);
				return start<TOut>.seq | value.Select((Func<TIn, TOut>) Selector);
			}
			
			return Seq<TOut>.SkipSeq;
		}
	}
}