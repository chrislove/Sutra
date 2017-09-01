using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public class InFunc<TIn> : SharpFunc<TIn, object> {
		[NotNull] internal new Func<TIn, object> Func => base.Func;

		internal InFunc( [NotNull] Func<TIn, object> func ) : base(i => func(i.To<TIn>())) {}

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static IInFunc<TIn> operator +( [NotNull] IOutFunc<TIn> lhs, [NotNull] InFunc<TIn> rhs)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			return lhs.Func + rhs;
		}
		
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static IInFunc<TIn> operator +([NotNull] Func<object, TIn> lhs, [NotNull] InFunc<TIn> rhs)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			return InFunc.FromFunc<TIn>(
			                             i => rhs.Func( lhs(i) )
			                            );
		}
	}
}