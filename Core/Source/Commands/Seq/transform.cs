

using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Transforms the entire sequence using a function on the right.
        /// </summary>
        /// <example><code>
        /// pipe | TRANSFORM | (e => e.Select(i => i + 1) )
        /// </code></example>
        public static DoTransform transform => new DoTransform();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoTransform { }
    
    public partial struct Seq<T> {
        public static DoTransform<T> operator |( Seq<T> pipe, DoTransform @do ) => new DoTransform<T>(pipe);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoTransform<T> {
        private readonly Seq<T> _pipe;

        internal DoTransform( Seq<T> pipe ) => _pipe = pipe;

        public static Seq<T> operator |( DoTransform<T> @do, [NotNull] Func<IEnumerable<T>, IEnumerable<T>> func )
            => func(@do._pipe.Get) | to<T>.pipe;
    }
}