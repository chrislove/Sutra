using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using Sutra.Transformations;
using static Sutra.Commands;

namespace Sutra {
    public static partial class Commands
    {
        /// <summary>
        /// Projects each element of a sequence and flattens the resulting sequences into one sequence. Equivalent to SelectMany() in LINQ.
        /// </summary>
        public static DoCollect<TIn, TOut> collectf<TIn, TOut>( Func<TIn, IEnumerable<TOut>> func ) => collectf(func.Map());
        
        /// <summary>
        /// Projects each element of a sequence and flattens the resulting sequences into one sequence. Equivalent to SelectMany() in LINQ.
        /// </summary>
        public static DoCollect<TIn, TOut> collectf<TIn, TOut>( Func<Option<TIn>, SeqOption<TOut>> func) => new DoCollect<TIn, TOut>(func);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoCollect<TIn, TOut> {
        private readonly Func<Option<TIn>, SeqOption<TOut>> _func;

        internal DoCollect( [NotNull] Func<Option<TIn>, SeqOption<TOut>> func ) => _func = func ?? throw new ArgumentNullException(nameof(func));

        public static Seq<TOut> operator |( Seq<TIn> seq, DoCollect<TIn, TOut> doCollect )
            {
                Func<IEnumerable<Option<TIn>>, SeqOption<TOut>> mapFunc = enm => enm.Select(doCollect._func).Flatten();
                
                return start.seq.of<TOut>() | seq.Option.Match( enm => mapFunc(enm), default(SeqOption<TOut>) );
            }
    }
}