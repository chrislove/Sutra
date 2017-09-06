using System;
using JetBrains.Annotations;

namespace SharpPipe {
    internal sealed class PipeNullException : SharpPipeException {
        public PipeNullException( string message ) : base(message) { }
        [NotNull]
        public static PipeNullException ForObject<TObject>(Type contextType)
            => new PipeNullException($"A null object of type {typeof(TObject)} was returned while operating on {contextType}");
        
        [NotNull]
        public static PipeNullException ForFunction<TFunc>()
            => new PipeNullException($"The function of type {typeof(TFunc)} is null.");
    }
}