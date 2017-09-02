using JetBrains.Annotations;
using System;

namespace SharpPipe
{
	public abstract partial class SharpFunc : ISharpFunc {
		[NotNull] public Func<object, object> Func { get; }

		protected SharpFunc([NotNull] Func<object, object> func, [CanBeNull] Type inType = null, [CanBeNull] Type outType = null ) {
			Func = func ?? throw new ArgumentNullException(nameof(func));

			InType = inType;
			OutType = outType;
		}

		/// <summary>
		/// Returns wrapped function.
		/// </summary>
		[NotNull]
		public static Func<object, object> operator ~(SharpFunc sharpFunc) => sharpFunc.Func;

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
				throw new TypeMismatchException(GetType(), type, InType, "In");
		}

		public void ValidateOutType(Type type) {
			if (type == null) return;

			if (OutType == null) return;

			if (OutType != type)
				throw new TypeMismatchException(GetType(), type, OutType, "Out");
		}

		[NotNull]
		internal static SharpFunc<TIn, TOut> FromFunc<TIn, TOut>( [NotNull] Func<TIn, TOut> func )
																		=> new SharpFunc<TIn, TOut>(func);

		[NotNull] internal static SharpFunc<TOut> FromFunc<TOut>([NotNull] Func<object, TOut> func) => new SharpFunc<TOut>(func);
		[NotNull]
		internal static SharpFunc<TOut> FromFunc<TOut>([NotNull] SharpFunc sharpFunc)
													=> new SharpFunc<TOut>(i => sharpFunc.Func(i).To<TOut>());

		/// <summary>
		/// Creates a SharpFunc that contains the input value.
		/// </summary>
		/// <param name="obj">Value to wrap</param>
		[NotNull]
		public static SharpFunc<TOut> WithValue<TOut>(TOut obj) => SharpFunc.FromFunc(i => obj);
	}
}