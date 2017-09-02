using System;
using JetBrains.Annotations;

namespace SharpPipe {
    public partial struct SharpFunc<TOut> : IOutFunc<TOut> {
        public Func<object, TOut> Func { get; }

        internal SharpFunc( [NotNull] Func<object, TOut> func ) {
            Func = func ?? throw new ArgumentNullException(nameof(func));
        }

        Func<object, object> ISharpFunc.Func {
            get {
                var @this = this;

                return i => @this.Func(i);
            }
        }
    }
}