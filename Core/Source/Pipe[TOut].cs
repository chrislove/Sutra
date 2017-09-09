using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace SharpPipe {
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct Pipe<TOut> : IPipe<TOut> {
        [NotNull] private Func<TOut> Func { get; }

        internal Pipe( [CanBeNull] TOut obj ) : this(() => obj) {
            if (obj == null && !PIPE.AllowNullInput)
                throw new NullPipeException($"Null object is not a valid input to {this.T()}");
        }

        private Pipe( [NotNull] Func<TOut> func ) : this() => Func = func ?? throw new ArgumentNullException(nameof(func));

        [NotNull] internal TOut Get {
            get {
                var result = Func();

                if (result == null && !PIPE.AllowNullOutput && !AllowNullOutput)
                    throw new NullPipeException($"'{this.T()}.Get' returned a null IEnumerable");

                return result;
            }
        }

        public bool AllowNullOutput { get; private set; }
    }
}