using System;
using JetBrains.Annotations;

namespace SharpPipe {
	internal static class TypeExtensions {
		/// <summary>
		/// Casts object to a given type.
		/// </summary>
		[CanBeNull]
		public static T To<T>( [CanBeNull] this object obj ) {
			if (obj == null) return default;

			try {
				return (T) obj;
			} catch (Exception) {
				throw new TypeMismatchException(obj.GetType(), typeof(T));
			}
		}
	}
}