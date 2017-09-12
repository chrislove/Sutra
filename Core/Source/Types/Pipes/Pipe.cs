using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    /// <summary>
    /// A pipe containing a single object.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct Pipe<T> : IPipe<T> {
        [CanBeNull]
        private readonly T _contents;
        
        private readonly bool _shouldSkip;

        internal Pipe( [CanBeNull] T value, bool shouldSkip = false ) : this() {
            _contents   = value;
            _shouldSkip = shouldSkip || value == null;
        }
        
        internal PipeOutput<T> Get => PipeOutput.New(_contents, _shouldSkip);

        internal static Pipe<T> SkipPipe => new Pipe<T>();

        private bool AllowNullOutput { get; set; }
        
        internal Pipe<TOut> Transform<TOut>([NotNull] Func<T, TOut> func) {
            var seqOut = this.Get;
            if (seqOut.ShouldSkip) return Pipe<TOut>.SkipPipe;

            return start<TOut>.pipe | func(seqOut.Contents);
        }

        /// <summary>
        /// Transforms pipe contents using a function on the right.
        /// </summary>
        public static Pipe<T> operator |( Pipe<T> pipe, Func<T, T> func ) => pipe.Transform(func);
        
        /// <summary>
        /// Replaces pipe contents with object on the right.
        /// </summary>
        public static Pipe<T> operator |( Pipe<T> _, T obj ) => start<T>.pipe | obj;
    }
}