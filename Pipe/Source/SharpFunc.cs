using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	public abstract class SharpFunc : ISharpFunc {
		[NotNull] public Func<object, object> Func { get; }

		protected SharpFunc([NotNull] Func<object, object> func, [CanBeNull] Type inType = null, [CanBeNull] Type outType = null ) {
			Func = func ?? throw new ArgumentNullException(nameof(func));

			InType = inType;
			OutType = outType;
		}

		/// <summary>
		/// Used for validation, in case the in type is known.
		/// </summary>
		public Type InType  { get; }
		public Type OutType { get; }

		public void ValidateCompatibilityWith( ISharpFunc rhsFunc ) {
			rhsFunc.ValidateInType(OutType);
			this.ValidateOutType(rhsFunc.InType);
		}

		public void ValidateInType(Type type) {
			if (type == null || InType == null) return;

			if (InType != type)
				throw new TypeMismatchException(GetType(), type, InType);
		}

		public void ValidateOutType(Type type) {
			if (type == null) return;

			if (OutType == null) return;

			if (OutType != type)
				throw new TypeMismatchException(GetType(), type, OutType);
		}

		[NotNull]
		internal static SharpFunc<TIn, TOut> FromFunc<TIn, TOut>( [NotNull] Func<TIn, TOut> func )
																		=> new SharpFunc<TIn, TOut>(func);

		/// <summary>
		/// Function composition operator
		/// </summary>
		[NotNull]
		public static SharpAct<object> operator +([NotNull] SharpFunc lhs, [NotNull] Action<object> rhs)
		{
			if (lhs == null) throw new ArgumentNullException(nameof(lhs));
			if (rhs == null) throw new ArgumentNullException(nameof(rhs));

			// Type validation not needed

			return SharpAct.FromAction( lhs.Func.CombineWith(rhs) );
		}
	}
}