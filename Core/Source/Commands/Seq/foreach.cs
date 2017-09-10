using System;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public partial struct Seq<T> {
        public static DoForEach<T> operator |( Seq<T> pipe, DoAct doAct )
            => new DoForEach<T>( pipe );
    }

    public struct DoForEach<T> {
        private readonly Seq<T> _pipe;

        public DoForEach( Seq<T> lhs ) => _pipe = lhs;

        public static Unit operator |( DoForEach<T> lhs, [NotNull] Action<T> action )
            => ( () => lhs._pipe.get.ForEach(action) ) | unit;
    }
}
