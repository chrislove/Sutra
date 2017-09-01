using System;

namespace SharpPipe {
	public sealed class TypeMismatchException : Exception {
		public TypeMismatchException( Type objectType, Type expectedType, Type actualType, string details)
			: base($"Type mismatch on object of type {objectType}: Expected {expectedType}, but found {actualType}. Details: {details}") {}

		public TypeMismatchException( Type fromType, Type toType)
			: base($"Type mismatch while trying to cast {fromType} to {toType}") {}
	}
}