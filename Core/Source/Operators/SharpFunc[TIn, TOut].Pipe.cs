using JetBrains.Annotations;
using System.Linq;
using static SharpPipe.Commands;

namespace SharpPipe
{
	public partial struct SharpFunc<TIn, TOut> {
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static Pipe<TOut> operator |( Pipe<TIn> pipe, SharpFunc<TIn, TOut> rhs ) => new Pipe<TOut>(rhs.Func(pipe.Get));
		
		/// <summary>
		/// Forward pipe operator. Transforms an EnumerablePipe.
		/// </summary>
		[UsedImplicitly]
		public static EnumPipe<TOut> operator |( EnumPipe<TIn> pipe, SharpFunc<TIn, TOut> func ) {
			var enumerable = pipe.Get.Select(i => func.Func(i).To<TOut>($"{pipe.T()} | {func.T()}"));

			return enumerable | TO<TOut>.PIPE;
		}
	}
}