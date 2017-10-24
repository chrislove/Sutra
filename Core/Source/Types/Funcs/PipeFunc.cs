using System;
using JetBrains.Annotations;
using SharpPipe.Transformations;

namespace SharpPipe
{
	/// <summary>
	/// Function transforming pipe or sequence value.
	/// </summary>
	public partial struct PipeFunc<TIn, TOut> {
		/// <summary>
		/// Inner function
		/// </summary>
		[PublicAPI]
		[NotNull] private Func<Option<TIn>, Option<TOut>> Func { get; }

		public PipeFunc([NotNull] Func<TIn, TOut> func) => Func = option => option.Map(func);
		public PipeFunc([NotNull] Func<TIn, Option<TOut>> func) => Func = option => option.Map(func).ValueOr(default);
		public PipeFunc([NotNull] Func<Option<TIn>, Option<TOut>> func) => Func = func ?? throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Use this operator to invoke the function.
		/// </summary>
		public Option<TOut> this[ [CanBeNull] TIn invalue ] => Func(invalue.ToOption());
		public Option<TOut> this[ Option<TIn> invalue ] => Func(invalue);

		public Func<Option<TIn>, TOut> ValueOr( TOut defaultOut ) => Func.ValueOr(defaultOut);

		/// <summary>
		/// Returns the contained function.
		/// </summary>
		[NotNull]
		public static Func<Option<TIn>, Option<TOut>> operator !( PipeFunc<TIn, TOut> pipeFunc ) => pipeFunc.Func;
		
		[NotNull]
		public static implicit operator Func<Option<TIn>, Option<TOut>>( PipeFunc<TIn, TOut> pipeFunc ) => pipeFunc.Func;
		public static implicit operator PipeFunc<TIn, TOut>( [NotNull] Func<TIn, TOut> func ) => new PipeFunc<TIn, TOut>(func);
		public static implicit operator PipeFunc<TIn, TOut>( [NotNull] Func<Option<TIn>, Option<TOut>> func ) => new PipeFunc<TIn, TOut>(func);


		/// <summary>
		/// Transforms a pipe.
		/// </summary>
		public static Pipe<TOut> operator |( Pipe<TIn> pipe, PipeFunc<TIn, TOut> func ) => pipe.Map(func.Func);

		/*
		/// <summary>
		/// Transforms every value in a sequence.
		/// </summary>
		public static Seq<TOut> operator |( Seq<TIn> seq, PipeFunc<TIn, TOut> func ) => seq.Map(v => v.Select(func.Func));
*/
		
		/*
		public static Pipe<TOut> operator |( DoBind<TOut> doBind, PipeFunc<TIn, TOut> func ) {
			return doBind._seq.Map(enm => enm.SelectMany(func.Func));
			//return doBind._seq | (enm => enm.SelectMany(func.Func));
		}*/
	}
}