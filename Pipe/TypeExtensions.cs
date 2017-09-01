using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public static class TypeExtensions {
		/// <summary>
		/// Compares object with a given type.
		/// </summary>
		public static bool Is<T>( this Type type ) {
			return type == typeof(T);
		}

		/// <summary>
		/// Casts object to a given type.
		/// </summary>
		[NotNull]
		public static T To<T>( [NotNull] this object obj ) {
			if (obj == null) {
				if (typeof(T).IsValueType)
					throw new InvalidOperationException($"Unable to convert a null object to type {typeof(T)}");

				return default;
			}

			try {
				return (T) obj;
			} catch (Exception) {
				throw new InvalidOperationException($"Unable to cast type [{obj.GetType()}] to {typeof(T)}.");
			}
		}
	}
}