using System;

namespace SharpPipe {
	public sealed class TypeMismatchException : Exception {
		public TypeMismatchException( Type objectType, Type expectedType, Type actualType )
			: base($"Type mismatch on object of type {objectType}: Expected {expectedType}, but found {actualType}.") {}
	}
}