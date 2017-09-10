using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class func {
        public partial class takes<TIn> {
            public static fromseqfunc<TIn, TOut> FromSeq<TOut>( Func<IEnumerable<TIn>, TOut> func ) => new fromseqfunc<TIn, TOut>(func);
        }
    }
    
    public struct fromseqfunc<TIn, TOut> {
        [NotNull] private Func<IEnumerable<TIn>, TOut> Func { get; }
        
        public TOut this[ [CanBeNull] IEnumerable<TIn> invalue ] => Func(invalue);

        internal fromseqfunc([NotNull] Func<IEnumerable<TIn>, TOut> func) => Func = func ?? throw new ArgumentNullException(nameof(func));
        
        public static Pipe<TOut> operator |( Seq<TIn> pipe, fromseqfunc<TIn, TOut> func ) => func[pipe.get] | to<TOut>.pipe;
    }
}