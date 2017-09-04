using System;
using JetBrains.Annotations;

namespace SharpPipe {
    public partial struct SharpFunc<TOut> {
        public Func<object, TOut> Func { get; }

        internal SharpFunc( [NotNull] Func<object, TOut> func ) => Func = func;
        internal SharpFunc( [NotNull] Func<TOut> func )         => Func = i => func();
    }
}