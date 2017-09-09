using System;
using JetBrains.Annotations;

namespace SharpPipe {
    public partial struct PipeFunc<TOut> {
        public Func<object, TOut> Func { get; }

        internal PipeFunc( [NotNull] Func<object, TOut> func ) => Func = func;
        internal PipeFunc( [NotNull] Func<TOut> func )         => Func = i => func();
    }
}