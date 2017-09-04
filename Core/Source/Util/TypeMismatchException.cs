using System;

namespace SharpPipe {
	internal sealed class TypeMismatchException : SharpPipeException {
		public TypeMismatchException( string message ) : base(message) {}

		public TypeMismatchException( Type fromType, Type toType)
			: base($"Type mismatch while trying to cast {fromType} to {toType}") {}
	}
}