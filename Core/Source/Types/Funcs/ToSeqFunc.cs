using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        public static partial class func {
            public partial class takes<TIn> {
                public static ToSeqFunc<TIn, TOut> toseq<TOut>( [NotNull] Func<TIn, IEnumerable<TOut>> func ) => new ToSeqFunc<TIn, TOut>(func);
            }
        }
    }

    public struct ToSeqFunc<TIn, TOut> {
        [NotNull] private Func<TIn, IEnumerable<TOut>> Func { get; }
        
        public IEnumerable<TOut> this[ [CanBeNull] TIn invalue ] => Func(invalue);

        internal ToSeqFunc([NotNull] Func<TIn, IEnumerable<TOut>> func) => Func = func ?? throw new ArgumentNullException(nameof(func));
        
        public static Seq<TOut> operator |( Pipe<TIn> pipe, ToSeqFunc<TIn, TOut> func )
            => start<TOut>.pipe | func[pipe.Get];
    }
}