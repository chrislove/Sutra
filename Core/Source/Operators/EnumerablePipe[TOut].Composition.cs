using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed partial class EnumerablePipe<TOut> {
		/// <summary>
		/// Pipe composition operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe{T}
		/// </summary>
		[NotNull]
		public static EnumerablePipe<TOut> operator +([NotNull] EnumerablePipe<TOut> lhs, [NotNull] IEnumerable<TOut> rhs)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not needed

			return lhs + EnumerablePipe.FromEnumerable(rhs);
		}


		/// <summary>
		/// Pipe composition operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe{T}
		/// </summary>
		[NotNull]
		public static EnumerablePipe<TOut> operator +( [NotNull] EnumerablePipe<TOut> lhs, [NotNull] EnumerablePipe<TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not needed

			var combined = lhs.Get.Concat(rhs.Get);

			return EnumerablePipe.FromEnumerable(combined);
		}
	}
}