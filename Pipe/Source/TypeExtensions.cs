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
		[CanBeNull]
		public static T To<T>( [CanBeNull] this object obj ) {
			if (obj == null) return default;

			try {
				return (T) obj;
			} catch (Exception) {
				throw new InvalidOperationException($"Unable to cast object of type [{obj.GetType()}] to [{typeof(T)}]. Pipe type mismatch?");
			}
		}
	}
}