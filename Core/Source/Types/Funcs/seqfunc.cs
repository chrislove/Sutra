using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static class seqfunc{
        public class takes<TIn> {
            public static seqfunc<TIn, TOut> from<TOut>( [NotNull] Func<IEnumerable<TIn>, IEnumerable<TOut>> func ) => new seqfunc<TIn, TOut>(func);
        }
    }
    
    /// <summary>
    /// A function transforming an entire sequence.
    /// </summary>
    public struct seqfunc<TIn, TOut> {
        [NotNull] private Func<IEnumerable<TIn>, IEnumerable<TOut>> Func { get; }

        internal seqfunc([NotNull] Func<IEnumerable<TIn>, IEnumerable<TOut>> func) => Func = func ?? throw new ArgumentNullException(nameof(func));

        /// <summary>
        /// Use this operator to invoke the function.
        /// </summary>
        public IEnumerable<TOut> this[ [CanBeNull] IEnumerable<TIn> invalue ] => Func(invalue);

        [NotNull]
        public static implicit operator Func<IEnumerable<TIn>, IEnumerable<TOut>>( seqfunc<TIn, TOut> func ) => func.Func;
        public static implicit operator seqfunc<TIn, TOut>( [NotNull] Func<IEnumerable<TIn>, IEnumerable<TOut>> func ) => seqfunc.takes<TIn>.from(func);
		
        /// <summary>
        /// Forward pipe operator. Transforms an Sequence.
        /// </summary>
        [UsedImplicitly]
        public static Seq<TOut> operator |( Seq<TIn> pipe, seqfunc<TIn, TOut> func )
            => func[pipe.get] | to<TOut>.pipe;
    }
}