using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
	public struct EnumFunc<TIn, TOut> {
		[NotNull] public Func<TIn, IEnumerable<TOut>> Func { get; }

		internal EnumFunc( [NotNull] Func<TIn, IEnumerable<TOut>> func ) {
			Func = func ?? throw new ArgumentNullException(nameof(func));
		}

		/*
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static EnumPipe<TOut> operator |( Pipe<TIn> lhs, EnumFunc<TIn, IEnumerable<TOut>> rhs ) {
			var combined = lhs.Get.Concat(rhs.Get);

			return EnumPipe.FromEnumerable(combined);
		}*/
	}
}