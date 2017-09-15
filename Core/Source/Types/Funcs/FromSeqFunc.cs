using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class FuncFactory {
        public static partial class func {
            [PublicAPI]
            public partial class takes<TIn> {
                /// <summary>
                /// Creates a function converting a sequence to a pipe.
                /// </summary>
                public static FromSeqFunc<TIn, TOut> FromSeq<TOut>( [NotNull] Func<IEnumerable<TIn>, TOut> func )
                                                    => new FromSeqFunc<TIn, TOut>(func.Map());
                
                /// <summary>
                /// Creates a function converting a sequence to a pipe.
                /// </summary>
                public static FromSeqFunc<TIn, TOut> FromSeq<TOut>( [NotNull] Func<SeqOption<TIn>, Option<TOut>> func )
                                                    => new FromSeqFunc<TIn, TOut>(func);
            }
        }
    }

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
        public Option<TOut> this[ [CanBeNull] IEnumerable<TIn> invalue ] => Func(invalue.DblReturn());
        public Option<TOut> this[ SeqOption<TIn> invalue ] => Func(invalue);
        
        internal FromSeqFunc([NotNull] Func<SeqOption<TIn>, Option<TOut>> func) => Func = func ?? throw new ArgumentNullException(nameof(func));
  
        /// <summary>
        /// Returns the contained function.
        /// </summary>
        [NotNull]
        public static Func<SeqOption<TIn>, Option<TOut>> operator !( FromSeqFunc<TIn, TOut> pipeFunc ) => pipeFunc.Func;

        
        /// <summary>
        /// Transforms sequence to a pipe.
        /// </summary>
        public static Pipe<TOut> operator |(Seq<TIn> seq, FromSeqFunc<TIn, TOut> func) {
            return start<TOut>.pipe | func[seq.Option];
        }
    }
}