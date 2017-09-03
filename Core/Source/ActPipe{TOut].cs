using System;
using JetBrains.Annotations;
using static SharpPipe.Pipe;

namespace SharpPipe {
    public struct ActPipe<TOut> : IPipe {
        internal SharpFunc<TOut> Func { get; }
        SharpFunc<object> IPipe.Func => SharpFunc.FromFunc<object>(Func);

        internal ActPipe( [NotNull] ISharpFunc func ) => Func = SharpFunc.FromFunc<TOut>(func);
        internal ActPipe( [NotNull] Func<TOut> func ) => Func = SharpFunc.FromFunc(func);

        [CanBeNull] internal TOut Get => Func.Func(null);

		
        public static SharpAct operator |( ActPipe<TOut> lhs, SharpAct<object> rhs ) => lhs | _<object>(p => rhs.Action(p));

        public static SharpAct operator |( ActPipe<TOut> lhs, SharpAct<TOut> rhs ) => lhs | _<TOut>(p => rhs.Action(p));
        
        public static SharpAct operator |( ActPipe<TOut> lhs, Action<TOut> rhs ) => lhs | _<TOut>(p => rhs(p));
    }
}