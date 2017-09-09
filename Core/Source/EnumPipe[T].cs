using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct EnumPipe<T> : IPipe<T> {
        internal static EnumPipe<T> Empty => new EnumPipe<T>(Enumerable.Empty<T>());

        internal EnumPipe( [CanBeNull] IEnumerable<T> obj ) : this( () => obj) {
            if (obj == null)
                throw new NullPipeException($"Null IEnumerable is not a valid input to {this.T()}");
        }

        private EnumPipe( [NotNull] Func<IEnumerable<T>> func ) : this() => Func = func ?? throw new ArgumentNullException(nameof(func));

        [NotNull] private Func<IEnumerable<T>> Func { get; }

        [NotNull]
        internal IEnumerable<T> Get {
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