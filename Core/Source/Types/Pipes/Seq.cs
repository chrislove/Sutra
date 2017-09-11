using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    /// <summary>
    /// A pipe containing a set of objects.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct Seq<T> : IPipe<T> {
        [NotNull] private readonly IEnumerable<T> _contents;

        internal static Seq<T> Empty => start<T>.pipe | Enumerable.Empty<T>();

        internal Seq( [NotNull] IEnumerable<T> sequence ) : this() {
            _contents = sequence ?? throw new NullPipeException($"Null IEnumerable is not a valid input to {nameof(Seq<T>)}");
        }

        [NotNull]
        internal IEnumerable<T> Get {
            get {
                if (_contents == null && !Pipe.AllowNullOutput && !AllowNullOutput)
                    throw new NullPipeException($"'{this.T()}.Get' returned a null IEnumerable");

                return _contents;
            }
        }

        private bool AllowNullOutput { get; set; }
    }
}