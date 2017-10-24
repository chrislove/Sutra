using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SharpPipe.Transformations;
using static SharpPipe.Commands;

namespace SharpPipe {
    /// <summary>
    /// Function transforming sequence to a pipe.
    /// </summary>
    public struct FromSeqFunc<TIn, TOut> {
        /// <summary>
        /// Inner function
        /// </summary>
        [PublicAPI]
        [NotNull] public Func<SeqOption<TIn>, Option<TOut>> Func { get; }
        
        /// <summary>
        /// Use this operator to invoke the function.
        /// </summary>
        [PublicAPI]
        public Option<TOut> this[ [CanBeNull] IEnumerable<TIn> invalue ] => Func(invalue.Return().Return());
        public Option<TOut> this[ SeqOption<TIn> invalue ] => Func(invalue);
        
        public FromSeqFunc([NotNull] Func<SeqOption<TIn>, Option<TOut>> func) => Func = func ?? throw new ArgumentNullException(nameof(func));
  
        /// <summary>
        /// Returns the contained function.
        /// </summary>
        [NotNull]
        public static Func<SeqOption<TIn>, Option<TOut>> operator !( FromSeqFunc<TIn, TOut> pipeFunc ) => pipeFunc.Func;

        
        /// <summary>
        /// Transforms sequence to a pipe.
        /// </summary>
        public static Pipe<TOut> operator |(Seq<TIn> seq, FromSeqFunc<TIn, TOut> func) {
            return start.pipe.of<TOut>() | func[seq.Option];
        }
    }
}