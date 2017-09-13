using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        public static partial class func {
            public partial class takes<TIn> {
                public static ToSeqFunc<TIn, TOut> toseq<TOut>( [NotNull] Func<TIn, IEnumerable<TOut>> func ) => new ToSeqFunc<TIn, TOut>(func.Lift());
            }
        }
    }

    /// <summary>
    /// Function transforming pipe to a sequence.
    /// </summary>
    public struct ToSeqFunc<TIn, TOut> {
        /// <summary>
        /// Inner function
        /// </summary>
        [PublicAPI]
        [NotNull] public Func<Option<TIn>, EnmOption<TOut>> Func { get; }

        /// <summary>
        /// Use this operator to invoke the function.
        /// </summary>
        [CanBeNull] [PublicAPI] public IEnumerable<TOut> this[ [CanBeNull] TIn invalue ] => Func.Lower()(invalue);
        public EnmOption<TOut> this[ Option<TIn> invalue ] => Func(invalue);

        internal ToSeqFunc([NotNull] Func<Option<TIn>, EnmOption<TOut>> func) => Func = func ?? throw new ArgumentNullException(nameof(func));
        
        /// <summary>
        /// Transforms pipe to a sequence using function on the right.
        /// </summary>
        public static Seq<TOut> operator |( Pipe<TIn> pipe, ToSeqFunc<TIn, TOut> func ) {
            foreach (var value in pipe.Option)
                return start<TOut>.seq | func[value];

            return Seq<TOut>.SkipSeq;
        }
    }
}