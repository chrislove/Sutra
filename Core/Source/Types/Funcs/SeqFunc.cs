using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static Sutra.Commands;

namespace Sutra {
    public static partial class Commands {
        public static class seqfunc {
            [PublicAPI]
            public class takes<TIn> {
                public static SeqFunc<TIn, TOut> from<TOut>( [NotNull] Func<IEnumerable<TIn>, IEnumerable<TOut>> func ) => new SeqFunc<TIn, TOut>(func);
            }
        }
    }

    /// <summary>
    /// Function transforming an entire sequence.
    /// </summary>
    public struct SeqFunc<TIn, TOut> {
        /// <summary>
        /// Inner function
        /// </summary>
        [PublicAPI]
        [NotNull] public Func<IEnumerable<TIn>, IEnumerable<TOut>> Func { get; }

        internal SeqFunc([NotNull] Func<IEnumerable<TIn>, IEnumerable<TOut>> func) => Func = func ?? throw new ArgumentNullException(nameof(func));

        /// <summary>
        /// Use this operator to invoke the function.
        /// </summary>
        [PublicAPI]
        public IEnumerable<TOut> this[ [CanBeNull] IEnumerable<TIn> invalue ] => Func(invalue);
        
        /// <summary>
        /// Returns the contained function.
        /// </summary>
        [NotNull]
        public static Func<IEnumerable<TIn>, IEnumerable<TOut>> operator !( SeqFunc<TIn, TOut> pipeFunc ) => pipeFunc.Func;

        
        [NotNull]
        public static implicit operator Func<IEnumerable<TIn>, IEnumerable<TOut>>( SeqFunc<TIn, TOut> func ) => func.Func;
        public static implicit operator SeqFunc<TIn, TOut>( [NotNull] Func<IEnumerable<TIn>, IEnumerable<TOut>> func ) => seqfunc.takes<TIn>.from(func);

        /// <summary>
        /// Transforms a sequence.
        /// </summary>
        [UsedImplicitly]
        public static Seq<TOut> operator |( Seq<TIn> seq, SeqFunc<TIn, TOut> func )
            => seq.Map<TOut>(func);
    }
}