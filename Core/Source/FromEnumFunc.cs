using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        public static partial class FUNC<TIn> {
            public static FromEnumFunc<TIn, TOut> FromEnum<TOut>( Func<IEnumerable<TIn>, TOut> func ) => new FromEnumFunc<TIn, TOut>(func);
        }
    }
    
    public struct FromEnumFunc<TIn, TOut> {
        [NotNull] private Func<IEnumerable<TIn>, TOut> Func { get; }

        internal FromEnumFunc([NotNull] Func<IEnumerable<TIn>, TOut> func) => Func = func ?? throw new ArgumentNullException(nameof(func));
        
        public static Pipe<TOut> operator |( EnumerablePipe<TIn> pipe, FromEnumFunc<TIn, TOut> func )
            => func.Func(pipe.Get) | TO<TOut>.PIPE;
    }
}