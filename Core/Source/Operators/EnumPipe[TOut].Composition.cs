using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial class EnumPipe<TOut> {
		/// <summary>
		/// Pipe composition operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe{T}
		/// </summary>
		[NotNull]
		public static EnumPipe<TOut> operator +([NotNull] EnumPipe<TOut> lhs, [NotNull] IEnumerable<TOut> rhs)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			return lhs + EnumPipe.FromEnumerable(rhs);
		}


		/// <summary>
		/// Pipe composition operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe{T}
		/// </summary>
		[NotNull]
		public static EnumPipe<TOut> operator +( [NotNull] EnumPipe<TOut> lhs, [NotNull] EnumPipe<TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			var combined = lhs.Get.Concat(rhs.Get);

			return EnumPipe.FromEnumerable(combined);
		}
	}
}