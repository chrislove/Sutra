using System;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial class SharpFunc<TIn, TOut> {
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static GetPipe<TOut> operator |( [NotNull] GetPipe<TIn> lhs, [NotNull] SharpFunc<TIn, TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not needed

			return GetPipe.FromFunc(lhs.Func + rhs);
		}

		/// <summary>
		/// Forward pipe operator. Transforms an EnumerablePipe.
		/// </summary>
		[NotNull]
		[UsedImplicitly]
		public static EnumerablePipe<TOut> operator |( [NotNull] EnumerablePipe<TIn> lhs, [NotNull] SharpFunc<TIn, TOut> func ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (func == null) throw new ArgumentNullException(nameof(func));

			// Type validation not needed

			var enumerable = lhs.Get.Select(i => func.Func(i).To<TOut>());

			return EnumerablePipe.FromEnumerable(enumerable);
		}
	}
}