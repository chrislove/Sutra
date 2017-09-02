using JetBrains.Annotations;
using System;
using System.Linq;

namespace SharpPipe
{
	public partial class SharpFunc<TIn, TOut> {
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static Pipe<TOut> operator |( [NotNull] Pipe<TIn> lhs, [NotNull] SharpFunc<TIn, TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			return Pipe.FromFunc(lhs.Func + rhs);
		}

		/// <summary>
		/// Forward pipe operator. Transforms an EnumerablePipe.
		/// </summary>
		[NotNull]
		[UsedImplicitly]
		public static EnumPipe<TOut> operator |( [NotNull] EnumPipe<TIn> lhs, [NotNull] SharpFunc<TIn, TOut> func ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (func == null) throw new ArgumentNullException(nameof(func));

			var enumerable = lhs.Get.Select(i => func.Func(i).To<TOut>());

			return EnumPipe.FromEnumerable(enumerable);
		}
	}
}