using System;
using JetBrains.Annotations;
using static SharpPipe.Pipe;

namespace SharpPipe {
    public struct ActPipe<TOut> : IPipe {
        private SharpFunc<TOut> Func { get; }
        SharpFunc<object> IPipe.Func => SharpFunc.FromFunc<object>(Func);

        internal ActPipe( [NotNull] ISharpFunc func ) => Func = SharpFunc.FromFunc<TOut>(func);
        internal ActPipe( [NotNull] Func<TOut> func ) => Func = SharpFunc.FromFunc(func);

        [CanBeNull] private TOut Get => Func.Func(null);

		
        public static SharpAct operator |( ActPipe<TOut> lhs, SharpAct<object> rhs ) => lhs | (p => rhs.Action(p));
        public static SharpAct operator |( ActPipe<TOut> lhs, SharpAct<TOut> rhs )   => lhs | (p => rhs.Action(p));
        
        public static SharpAct operator |( ActPipe<TOut> lhs, Action<TOut> rhs ) {
            return SharpAct.FromAction( () => rhs( lhs.Get ) );
        }
    }
}