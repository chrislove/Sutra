using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	public class SharpFunc {
		protected SharpFunc( [NotNull] Func<object, object> func ) {
			_func = func ?? throw new ArgumentNullException(nameof(func));
		}

		public SharpFunc( [CanBeNull] object value) : this(i => value) {	}

		[NotNull] private readonly Func<object, object> _func;

		[NotNull]
		private Func<object, object> GetFunc() => i => _func(i);

		[NotNull]
		internal Func<TIn, TOut> GetFunc<TIn, TOut>() => i => _func(i).To<TOut>();

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc operator +( [NotNull] F lhs, [NotNull] SharpFunc rhs) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			Func<object, object> lhsFunc = i => lhs(i);
			Func<object, object> rhsFunc = rhs.GetFunc();

			Func<object, object> combined = i => rhsFunc(lhsFunc(i));

			return new SharpFunc(combined);
		}
	}
}