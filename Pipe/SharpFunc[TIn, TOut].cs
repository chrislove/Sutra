using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed class SharpFunc<TIn, TOut> : SharpFunc<TIn> {
		[NotNull] private readonly Func<TIn, TOut> _func;

		[NotNull]
		private Func<TIn, TOut> GetFunc() => _func;

		internal SharpFunc( [NotNull] Func<TIn, TOut> func ) : base(i => func(i)) {
			_func = func ?? throw new ArgumentNullException(nameof(func));
		}

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc<object, TOut> operator +( [NotNull] SharpFunc lhs, [NotNull] SharpFunc<TIn, TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			Func<object, TIn> lhsFunc    = lhs.GetFunc<object, TIn>();
			Func<TIn, TOut>      rhsFunc = rhs.GetFunc();

			Func<object, TOut> combined = i => rhsFunc( lhsFunc(i) );

			return new SharpFunc<object, TOut>(combined);
		}
		
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc<object, TOut> operator +( [NotNull] F lhs, [NotNull] SharpFunc<TIn, TOut> rhs ) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			Func<object, TIn> lhsFunc    = i => lhs(i).To<TIn>();
			Func<TIn, TOut>      rhsFunc = rhs.GetFunc();

			Func<object, TOut> combined = i => rhsFunc( lhsFunc(i) );

			return new SharpFunc<object, TOut>(combined);
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static GetPipe<TOut> operator |( [NotNull] GetPipe lhs, [NotNull] SharpFunc<TIn, TOut> rhs) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			var combined = lhs.Func + rhs;

			return new GetPipe<TOut>(combined.GetFunc<object, TOut>());
		}
	}
}