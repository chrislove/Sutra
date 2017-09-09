using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct EnumPipe<TOut> : IPipe<TOut> {
        internal static EnumPipe<TOut> Empty => new EnumPipe<TOut>(Enumerable.Empty<TOut>());

        internal EnumPipe( [CanBeNull] IEnumerable<TOut> obj ) : this( () => obj) {
            if (obj == null)
                throw new NullPipeException($"Null IEnumerable is not a valid input to {this.T()}");
        }

        private EnumPipe( [NotNull] Func<IEnumerable<TOut>> func ) : this() => Func = func ?? throw new ArgumentNullException(nameof(func));

        [NotNull] private Func<IEnumerable<TOut>> Func { get; }

        [NotNull]
        internal IEnumerable<TOut> Get {
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