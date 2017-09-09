// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Transforms the entire EnumerablePipe using a function on the right.
        /// </summary>
        /// <example><code>
        /// pipe | TRANSFORM | (e => e.Select(i => i + 1) )
        /// </code></example>
        public static DoTransform TRANSFORM => new DoTransform();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoTransform { }
    
    public partial struct EnumerablePipe<T> {
        public static DoTransform<T> operator |( EnumerablePipe<T> pipe, DoTransform @do ) => new DoTransform<T>(pipe);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoTransform<T> {
        private readonly EnumerablePipe<T> _pipe;

        internal DoTransform( EnumerablePipe<T> pipe ) => _pipe = pipe;

        public static EnumerablePipe<T> operator |( DoTransform<T> @do, [NotNull] Func<IEnumerable<T>, IEnumerable<T>> func )
            => func(@do._pipe.Get) | TO<T>.PIPE;
    }
}