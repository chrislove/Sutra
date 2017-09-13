using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Projects each element of a sequence and flattens the resulting sequences into one sequence.
        /// Equivalent to SelectMany() in LINQ.
        /// </summary>
        /// <list type="bullet">
        /// <item><description>For Seq{T}: Projects each element of a sequence and flattens the resulting sequences into one sequence. Equivalent to SelectMany() in LINQ.</description></item>
        /// <item><description>For Pipe{T}: Transforms pipe to a sequence.</description></item>
        /// </list>
        /// <example>
        /// <code>
        /// seq | bind | (i => Enumerable.Repeat(i, 3))
        /// </code>
        /// </example>
        public static DoBind bind => new DoBind();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoBind { }
    
    public partial struct Seq<T> {
        public static DoBind<T> operator |( Seq<T> seq, DoBind _ ) => new DoBind<T>(seq);
    }
    
    /*
    public partial struct Pipe<T> {
        public static DoBind<T> operator |( Pipe<T> pipe, DoBind _ ) => new DoBind<T>(pipe);
    }*/
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoBind<T> {
        internal readonly Seq<T> _seq;

        internal DoBind( Seq<T> pipe ) => this._seq = pipe;

        public static Seq<T> operator |( DoBind<T> doSelect, [NotNull] Func<T, IEnumerable<T>> func )
            => doSelect._seq | (enm => enm.SelectMany(func));
    }
}