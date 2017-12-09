using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace Sutra {
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class Command<T> {
        [NotNull]
        internal readonly IPipe<T> Pipe;

        internal Command( [NotNull] IPipe<T> pipe ) => Pipe = pipe ?? throw new ArgumentNullException(nameof(pipe));
        
        internal Command( [NotNull] Command<T> copyFrom ) {
                Pipe = copyFrom.Pipe ?? throw new ArgumentNullException(nameof(copyFrom));
        }
    }
}