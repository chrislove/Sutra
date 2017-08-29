using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public class SharpFunc<TIn> : SharpFunc {
		[NotNull] private readonly Func<TIn, object> _func;

		[NotNull]
		private Func<TIn, object> GetFunc() => _func;

		internal SharpFunc( [NotNull] Func<TIn, object> func ) : base(i => func(i.To<TIn>())) {
			_func = func ?? throw new ArgumentNullException(nameof(func));
		}

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc<object> operator +( [NotNull] SharpFunc lhs, [NotNull] SharpFunc<TIn> rhs)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			Func<object, TIn> lhsFunc = lhs.GetFunc<object, TIn>();
			Func<TIn, object> rhsFunc = rhs.GetFunc();

			Func<object, object> combined = i => rhsFunc( lhsFunc(i) );

			return new SharpFunc<object>(combined);
		}
		
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc<object> operator +( [NotNull] F lhs, [NotNull] SharpFunc<TIn> rhs)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			Func<object, TIn> lhsFunc = i => lhs(i).To<TIn>();
			Func<TIn, object> rhsFunc = rhs.GetFunc();

			Func<object, object> combined = i => rhsFunc( lhsFunc(i) );

			return new SharpFunc<object>(combined);
		}
	}
}