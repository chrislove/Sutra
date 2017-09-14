using System;
using JetBrains.Annotations;

namespace SharpPipe {
	internal static class TypeExtensions {
		/// <summary>
		/// Casts object to a given type.
		/// </summary>
		[CanBeNull]
		public static T To<T>( [CanBeNull] this object obj, [CanBeNull] object lhsContext, [CanBeNull] object rhsContest ) {
			if (obj == null) return default(T);

			try {
				return (T) obj;
			} catch (Exception) {
				string context = $"{lhsContext?.GetType()} | {rhsContest?.GetType()}";
				throw new TypeMismatchException(obj.GetType(), typeof(T), context);
			}
		}

		/// <summary>
		/// Casts object to a given type.
		/// </summary>
		[CanBeNull]
		public static T To<T>( [CanBeNull] this object obj, [CanBeNull] string context ) {
			if (obj == null) return default(T);

			try {
				return (T) obj;
			} catch (Exception) {
				throw new TypeMismatchException(obj.GetType(), typeof(T), context);
			}
		}

		/// <summary>
		/// Returns the compile-time generic type
		/// </summary>
		[NotNull]
		public static Type T<U>([CanBeNull] this U _) => typeof(U);

		[CanBeNull]
		public static Type GetFirstGenericArg( [NotNull] this Type type )
			{
			if (type == null) throw new ArgumentNullException(nameof(type));
				if (!type.IsGenericType)
					throw new SharpPipeException($"Type {type} isn't generic.");
				
				return type.GenericTypeArguments[0];
			}
	}
}