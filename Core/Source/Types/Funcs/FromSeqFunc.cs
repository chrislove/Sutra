using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        public static partial class func {
            public partial class takes<TIn> {
                public static FromSeqFunc<TIn, TOut> FromSeq<TOut>( Func<IEnumerable<TIn>, TOut> func ) => new FromSeqFunc<TIn, TOut>(func);
            }
        }
    }

    public struct FromSeqFunc<TIn, TOut> {
        [NotNull] private Func<IEnumerable<TIn>, TOut> Func { get; }
        
        public TOut this[ [CanBeNull] IEnumerable<TIn> invalue ] => Func(invalue);

        internal FromSeqFunc([NotNull] Func<IEnumerable<TIn>, TOut> func) => Func = func ?? throw new ArgumentNullException(nameof(func));
        
        public static Pipe<TOut> operator |( Seq<TIn> pipe, FromSeqFunc<TIn, TOut> func ) => start<TOut>.pipe | func[pipe.Get];
    }
}