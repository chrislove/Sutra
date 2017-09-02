using JetBrains.Annotations;
using System.Linq;

namespace SharpPipe
{
	public partial struct SharpFunc<TIn, TOut> {
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static Pipe<TOut> operator |( Pipe<TIn> lhs, SharpFunc<TIn, TOut> rhs ) => Pipe.FromFunc(lhs.Func + rhs);

		
		
		/// <summary>
		/// Forward pipe operator. Transforms an EnumerablePipe.
		/// </summary>
		[UsedImplicitly]
		public static EnumPipe<TOut> operator |( EnumPipe<TIn> lhs, SharpFunc<TIn, TOut> func ) {
			var enumerable = lhs.Get.Select(i => func.Func(i).To<TOut>());

			return EnumPipe.FromEnumerable(enumerable);
		}
	}
}