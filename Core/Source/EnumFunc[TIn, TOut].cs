using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
	/// <summary>
	/// Transforms IEnumerable{TIn} into {TOut}
	/// </summary>
	public struct EnumInFunc<TIn, TOut> : IOutFunc<TOut> {
		[NotNull] private Func<IEnumerable<TIn>, TOut> Func { get; }

		internal EnumInFunc( [NotNull] Func<IEnumerable<TIn>, TOut> func ) {
			Func = func ?? throw new ArgumentNullException(nameof(func));
		}
		
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		public static Pipe<TOut> operator |( EnumPipe<TIn> lhs, EnumInFunc<TIn, TOut> rhs ) {
			return Pipe.FromObject( rhs.Func(lhs.Get) );
		}

		Func<object, object> ISharpFunc.Func {
			get {
				var @this = this;
				return i => @this.Func(i.To<IEnumerable<TIn>>());
			}
		}

		Func<object, TOut> IOutFunc<TOut>.Func {
			get {
				var @this = this;
				return i => @this.Func(i.To<IEnumerable<TIn>>());
			}
		}
	}

	internal static class EnumInFunc {
		public static EnumInFunc<TIn, TOut> FromFunc<TIn, TOut>(Func<IEnumerable<TIn>, TOut> func) => new EnumInFunc<TIn, TOut>(func);
	}
}