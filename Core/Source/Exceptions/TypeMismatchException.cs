using System;

namespace Sutra {
	internal sealed class TypeMismatchException : SutraException {
		public TypeMismatchException( string message ) : base(message) {}

		public TypeMismatchException( Type fromType, Type toType, string context)
			: base($"Type mismatch while trying to cast {fromType} to {toType}, context: '{context}'") {}
	}
}