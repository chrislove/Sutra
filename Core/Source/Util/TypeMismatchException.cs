using System;

namespace SharpPipe {
	public sealed class TypeMismatchException : Exception {
		public TypeMismatchException( Type fromType, Type toType)
			: base($"Type mismatch while trying to cast {fromType} to {toType}") {}
	}
}