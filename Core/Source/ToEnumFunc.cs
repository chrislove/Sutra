using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        public static partial class FUNC<TIn> {
            public static ToEnumFunc<TIn, TOut> ToEnum<TOut>( Func<TIn, IEnumerable<TOut>> func ) => new ToEnumFunc<TIn, TOut>(func);
        }
    }

    public struct ToEnumFunc<TIn, TOut> {
        [NotNull] private Func<TIn, IEnumerable<TOut>> Func { get; }

        internal ToEnumFunc([NotNull] Func<TIn, IEnumerable<TOut>> func) => Func = func ?? throw new ArgumentNullException(nameof(func));
        
        public static EnumerablePipe<TOut> operator |( Pipe<TIn> pipe, ToEnumFunc<TIn, TOut> func )
            => func.Func(pipe.Get) | TO<TOut>.PIPE;
    }
}