using System;

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
		public static T To<T>( this object obj ) {
			try {
				return (T) obj;
			} catch (Exception) {
				throw new InvalidOperationException($"Unable to cast type [{obj.GetType()}] to {typeof(T)}.");
			}
		}
	}
}