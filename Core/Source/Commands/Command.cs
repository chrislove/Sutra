using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace SharpPipe {
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class Command<T> {
        [NotNull]
        internal readonly IPipe<T> Pipe;

        internal Command( [NotNull] IPipe<T> pipe ) => Pipe = pipe ?? throw new ArgumentNullException(nameof(pipe));
        
        internal Command( [NotNull] Command<T> copyFrom ) {
            if (copyFrom == null) throw new ArgumentNullException(nameof(copyFrom));
            
            Pipe = copyFrom.Pipe;
        }
    }
}