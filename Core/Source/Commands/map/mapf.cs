using System;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using Sutra.Transformations;

namespace Sutra {
    public static partial class Commands {
        public static DoMap<TIn, TOut> mapf<TIn, TOut>(Func<TIn, TOut> func) => new DoMap<TIn, TOut>(func);
        public static DoMap<TIn, TOut> mapf<TIn, TOut>(Func<Option<TIn>, Option<TOut>> func) => new DoMap<TIn, TOut>(func);
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoMap<TIn, TOut> {
        private readonly Func<Option<TIn>, Option<TOut >> _func;

        public DoMap( [NotNull] Func<TIn, TOut> func ) => _func = func.Map();
        public DoMap( [NotNull] Func<Option<TIn>, Option<TOut>> func ) => _func = func;

        public static Pipe<TOut> operator |( Pipe<TIn> pipe, DoMap<TIn, TOut> doMap )  => pipe.Map(doMap._func);
        public static Seq<TOut>  operator |( Seq<TIn> seq, DoMap<TIn, TOut> doMap )    => seq.Map( enm => enm.Select(doMap._func) );
    }
}