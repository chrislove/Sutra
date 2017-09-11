using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        public static class seqfunc {
            public class takes<TIn> {
                public static SeqFunc<TIn, TOut> from<TOut>( [NotNull] Func<IEnumerable<TIn>, IEnumerable<TOut>> func ) => new SeqFunc<TIn, TOut>(func);
            }
        }
    }

    /// <summary>
    /// A function transforming an entire sequence.
    /// </summary>
    public struct SeqFunc<TIn, TOut> {
        [NotNull] private Func<IEnumerable<TIn>, IEnumerable<TOut>> Func { get; }

        internal SeqFunc([NotNull] Func<IEnumerable<TIn>, IEnumerable<TOut>> func) => Func = func ?? throw new ArgumentNullException(nameof(func));

        /// <summary>
        /// Use this operator to invoke the function.
        /// </summary>
        public IEnumerable<TOut> this[ [CanBeNull] IEnumerable<TIn> invalue ] => Func(invalue);

        [NotNull]
        public static implicit operator Func<IEnumerable<TIn>, IEnumerable<TOut>>( SeqFunc<TIn, TOut> func ) => func.Func;
        public static implicit operator SeqFunc<TIn, TOut>( [NotNull] Func<IEnumerable<TIn>, IEnumerable<TOut>> func ) => seqfunc.takes<TIn>.from(func);
		
        /// <summary>
        /// Forward pipe operator. Transforms an Sequence.
        /// </summary>
        [UsedImplicitly]
        public static Seq<TOut> operator |( Seq<TIn> pipe, SeqFunc<TIn, TOut> func )
            => func[pipe.Get] | to<TOut>.pipe;
    }
}