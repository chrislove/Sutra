using System;
using JetBrains.Annotations;

namespace SharpPipe {
    internal sealed class EmptyPipeException : SharpPipeException {
        public EmptyPipeException( [CanBeNull] Type pipeType ) : base($"This operation is invalid on empty pipe of type {pipeType}") {}
        public EmptyPipeException( [CanBeNull] Type pipeType, string operationName ) : base($"Operation {operationName} is invalid on empty pipe of type {pipeType}") {}
        
        [NotNull] public static EmptyPipeException ForType<T>() => new EmptyPipeException(typeof(T));
        [NotNull] public static EmptyPipeException ForType<T>(string operationName) => new EmptyPipeException(typeof(T), operationName);
    }
}