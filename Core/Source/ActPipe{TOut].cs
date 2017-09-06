using System;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public struct ActPipe<TOut> {
        private SharpFunc<TOut> Func { get; }

        internal ActPipe( SharpFunc<TOut> func ) => Func = SharpFunc.FromFunc(func);
        internal ActPipe( [NotNull] Func<TOut> func ) => Func = SharpFunc.FromFunc(func);

        [CanBeNull] private TOut Get => Func.Func(null);
        
        public static VOID operator |( ActPipe<TOut> lhs, [NotNull] Action<TOut> rhs ) => VOID.VoidAction( () => rhs(lhs.Get) );
    }
}