using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace SharpPipe {
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class Command<T> {
        protected readonly IPipe<T> Pipe;

        protected Command( IPipe<T> pipe ) => Pipe = pipe ?? throw new ArgumentNullException(nameof(pipe));
        
        protected Command( [NotNull] Command<T> copyFrom ) {
            if (copyFrom == null) throw new ArgumentNullException(nameof(copyFrom));
            Pipe = copyFrom.Pipe;
        }
    }
}