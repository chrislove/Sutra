using System;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    /// <summary>
    /// A pipe monad containing a single object.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct Pipe<T> : IPipe<T> {
        internal Pipe( Option<T> value )   => Option = value;
        internal Pipe( T value )           => Option = value.ToOption();

        internal Option<T> Option { get; }

        internal static Pipe<T> SkipPipe => new Pipe<T>(Option<T>.None);

        internal Pipe<TOut> Bind<TOut>([NotNull] Func<T, TOut> func) {
            foreach (var value in Option)
                return start<TOut>.pipe | func(value);

            return Pipe<TOut>.SkipPipe;
        }
        
        internal Pipe<TOut> Bind<TOut>([NotNull] Func<Option<T>, Option<TOut>> func) => start<TOut>.pipe | func(Option);

        /// <summary>
        /// Transforms pipe contents using a function on the right.
        /// </summary>
        public static Pipe<T> operator |( Pipe<T> pipe, Func<T, T> func ) => pipe.Bind(func);
        
        /// <summary>
        /// Replaces pipe contents with object on the right.
        /// </summary>
        public static Pipe<T> operator |( Pipe<T> _, T obj ) => start<T>.pipe | obj;

        /// <summary>
        /// Replaces pipe contents with option on the right if it has value.
        /// </summary>
        public static Pipe<T> operator |( Pipe<T> pipe, Option<T> option ) => option.HasValue ? new Pipe<T>(option) : pipe;
    }
}