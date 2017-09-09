using System;
using JetBrains.Annotations;

namespace SharpPipe {
    public struct ActPipe<TOut> {
        private PipeFunc<TOut> Func { get; }

        internal ActPipe( PipeFunc<TOut> func ) => Func = PipeFunc.FromFunc(func);
        internal ActPipe( [NotNull] Func<TOut> func ) => Func = PipeFunc.FromFunc(func);

        [CanBeNull] private TOut Get => Func.Func(null);
        
        public static Unit operator |( ActPipe<TOut> lhs, [NotNull] Action<TOut> rhs ) => Unit.UnitAction( () => rhs(lhs.Get) );
    }
}