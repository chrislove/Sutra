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
    public partial struct Seq<T> : IPipe<T> {
        internal static Seq<T> empty => new Seq<T>(Enumerable.Empty<T>());

        internal Seq( [CanBeNull] IEnumerable<T> obj ) : this( () => obj) {
            if (obj == null)
                throw new NullPipeException($"Null IEnumerable is not a valid input to {this.T()}");
        }

        private Seq( [NotNull] Func<IEnumerable<T>> func ) : this() => this.func = func ?? throw new ArgumentNullException(nameof(func));

        [NotNull] private Func<IEnumerable<T>> func { get; }

        [NotNull]
        internal IEnumerable<T> get {
            get {
                var result = func();

                if (result == null && !PIPE.AllowNullOutput && !AllowNullOutput)
                    throw new NullPipeException($"'{this.T()}.Get' returned a null IEnumerable");

                return result;
            }
        }

        private bool AllowNullOutput { get; set; }
    }
}