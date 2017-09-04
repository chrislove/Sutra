using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
	/// <summary>
	/// Transforms IEnumerable{TIn} into {TOut}
	/// </summary>
	public struct EnumInFunc<TIn, TOut> {
		[NotNull] private Func<IEnumerable<TIn>, TOut> Func { get; }

		internal EnumInFunc( [NotNull] Func<IEnumerable<TIn>, TOut> func ) {
			Func = func ?? throw new ArgumentNullException(nameof(func));
		}
		
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static Pipe<TOut> operator -( EnumPipe<TIn> lhs, EnumInFunc<TIn, TOut> rhs ) {
			return PIPE.IN( rhs.Func(lhs.Get) );
		}
	}

	internal static class EnumInFunc {
		public static EnumInFunc<TIn, TOut> FromFunc<TIn, TOut>(Func<IEnumerable<TIn>, TOut> func) => new EnumInFunc<TIn, TOut>(func);
	}
}