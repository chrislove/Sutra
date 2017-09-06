using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
	internal static class TypeExtensions {
		/// <summary>
		/// Casts object to a given type.
		/// </summary>
		[CanBeNull]
		public static T To<T>( [CanBeNull] this object obj, string context ) {
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
		public static Type T<U>(this U obj) => typeof(U);

		internal static bool IsArrayOf<T>( this Type type ) => type.IsArray && typeof(T).IsAssignableFrom(type.GetElementType());
		internal static bool IsEnumerable<T>(this Type type) => type.IsAssignableFrom(typeof(IEnumerable<T>));
		internal static bool IsEnumPipe<T>(this Type type)   => type.IsAssignableFrom(typeof(EnumPipe<T>));
	}
}