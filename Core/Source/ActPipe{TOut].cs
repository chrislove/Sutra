using System;
using JetBrains.Annotations;

namespace SharpPipe {
    public struct ActPipe<TOut> {
        [NotNull] private Func<TOut> Func { get; }

        internal ActPipe( [NotNull] Func<TOut> func ) => Func = func ?? throw new ArgumentNullException(nameof(func));

        [CanBeNull] private TOut Get => Func();
        
        public static Unit operator |( ActPipe<TOut> lhs, [NotNull] Action<TOut> rhs ) => Unit.UnitAction( () => rhs(lhs.Get) );
    }
}