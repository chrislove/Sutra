using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class func {
        public partial class takes<TIn> {
            public static toseqfunc<TIn, TOut> toseq<TOut>( [NotNull] Func<TIn, IEnumerable<TOut>> func ) => new toseqfunc<TIn, TOut>(func);
        }
    }

    public struct toseqfunc<TIn, TOut> {
        [NotNull] private Func<TIn, IEnumerable<TOut>> Func { get; }
        
        public IEnumerable<TOut> this[ [CanBeNull] TIn invalue ] => Func(invalue);

        internal toseqfunc([NotNull] Func<TIn, IEnumerable<TOut>> func) => Func = func ?? throw new ArgumentNullException(nameof(func));
        
        public static Seq<TOut> operator |( Pipe<TIn> pipe, toseqfunc<TIn, TOut> func )
            => func[pipe.get] | to<TOut>.pipe;
    }
}