using System;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using SharpPipe.Transformations;

namespace SharpPipe {
    public static partial class Commands {
        public static DoBind<TIn, TOut> mapf<TIn, TOut>(Func<TIn, TOut> func) => new DoBind<TIn, TOut>(func);
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoBind<TIn, TOut> {
        private readonly Func<Option<TIn>, Option<TOut >> _func;

        public DoBind( [NotNull] Func<TIn, TOut> func ) => _func = func.Map();

        public static Pipe<TOut> operator |( Pipe<TIn> pipe, DoBind<TIn, TOut> doBind ) => pipe.Map(doBind._func);
        public static Seq<TOut> operator |( Seq<TIn> seq, DoBind<TIn, TOut> doBind )    => seq.Map( enm => enm.Select(doBind._func) );
    }
}