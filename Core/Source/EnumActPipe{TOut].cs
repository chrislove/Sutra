using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
    public struct EnumActPipe<TOut> {
        internal EnumActPipe([CanBeNull] IEnumerable<TOut> obj) : this(SharpFunc.WithValue(obj) ) { }

        internal EnumActPipe( SharpFunc<IEnumerable<TOut>> func ) => Func = func;

        private SharpFunc<IEnumerable<TOut>> Func { get; }

        [CanBeNull] internal IEnumerable<TOut> Get => Func.Func(default(IEnumerable<TOut>));
    }
}