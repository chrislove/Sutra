using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed class OutFunc<TOut> : SharpFunc<object, TOut> {
		[NotNull] internal new Func<object, TOut> Func => base.Func;

		internal OutFunc([NotNull] Func<object, TOut> func) : this(func, typeof(TOut)) { }

		private OutFunc( [NotNull] Func<object, TOut> func, Type outType ) : base(func, null, outType) {}
		
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static OutFunc<TOut> operator +([NotNull] SharpFunc lhs, [NotNull] OutFunc<TOut> rhs)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			lhs.ValidateCompatibilityWith(rhs);

			return ( i => lhs.Func(i) ) + rhs;
		}

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static OutFunc<TOut> operator +([NotNull] Func<object, object> lhs, [NotNull] OutFunc<TOut> rhs)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not possible

			return OutFunc.FromFunc(
			                        i => rhs.Func( lhs(i) )
			                       );
		}


		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpAct<TOut> operator +([NotNull] OutFunc<TOut> lhs, [NotNull] Action<TOut> rhs)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not needed

			return SharpAct.FromAction<TOut>(
			                               i => rhs( lhs.Func(i) )
			                              );
		}

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpAct<TOut> operator +([NotNull] OutFunc<TOut> lhs, [NotNull] SharpAct<TOut> rhs)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not needed

			return lhs + rhs.Action;
		}


		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		public static GetPipe<TOut> operator |([NotNull] GetPipe lhs, [NotNull] OutFunc<TOut> rhs)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			lhs.ValidateCompatibilityWith(rhs);

			return GetPipe.FromFunc(lhs.Func + rhs);
		}
	}
}