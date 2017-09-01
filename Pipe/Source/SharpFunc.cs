using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	public abstract class SharpFunc {
		[NotNull] internal Func<object, object> Func { get; }

		protected SharpFunc( [NotNull] Func<object, object> func ) {
			Func = func ?? throw new ArgumentNullException(nameof(func));
		}

		//[NotNull] internal static SharpFunc   FromFunc( [NotNull] Func<object, object> func ) => new SharpFunc(func);

		[NotNull]
		internal static SharpFunc<TIn, TOut> FromFunc<TIn, TOut>( [NotNull] Func<TIn, TOut> func )
																		=> new SharpFunc<TIn, TOut>(func);
		/*
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc operator +([NotNull] Func<object, object> lhs, [NotNull] SharpFunc rhs) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			object CombinedFunc( object i ) => rhs.Func(lhs(i));

			return SharpFunc.FromFunc(CombinedFunc);
		}
		
		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpFunc operator +([NotNull] SharpFunc lhs, [NotNull] SharpFunc rhs) {
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			return lhs.Func + rhs;
		}*/

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpAct<object> operator +([NotNull] SharpFunc lhs, [NotNull] Action<object> rhs)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			return SharpAct.FromAction( lhs.Func.CombineWith(rhs) );
		}
	}
}