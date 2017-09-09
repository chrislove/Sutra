using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    /// <summary>
    /// A pipe containing a set of objects.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct EnumerablePipe<T> : IPipe<T> {
        internal static EnumerablePipe<T> Empty => new EnumerablePipe<T>(Enumerable.Empty<T>());

        internal EnumerablePipe( [CanBeNull] IEnumerable<T> obj ) : this( () => obj) {
            if (obj == null)
                throw new NullPipeException($"Null IEnumerable is not a valid input to {this.T()}");
        }

        private EnumerablePipe( [NotNull] Func<IEnumerable<T>> func ) : this() => Func = func ?? throw new ArgumentNullException(nameof(func));

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

        private bool AllowNullOutput { get; set; }
    }
}