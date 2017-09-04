using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
	public partial struct EnumPipe<TOut> {
		/// <summary>
		/// Pipe composition operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe{T}
		/// </summary>
		public static EnumPipe<TOut> operator +(EnumPipe<TOut> lhs, [NotNull] IEnumerable<TOut> rhs)
		{
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			return lhs + EnumPipe.FromEnumerable(rhs);
		}
		
		/// <summary>
		/// Pipe composition operator, adds a new value to IEnumerable{T}.
		/// </summary>
		public static EnumPipe<TOut> operator +(EnumPipe<TOut> lhs, [CanBeNull] TOut rhs) => lhs + Yield(rhs);


		/// <summary>
		/// Pipe composition operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe{T}
		/// </summary>
		public static EnumPipe<TOut> operator +( EnumPipe<TOut> lhs, EnumPipe<TOut> rhs ) {
			var combined = lhs.Get.Concat(rhs.Get);

			return EnumPipe.FromEnumerable(combined);
		}
		
		private static IEnumerable<T> Yield<T>([CanBeNull] T item) {
			yield return item;
		}

	}
}