using System;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    /// <summary>
    /// A pipe containing a single object.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct Pipe<T> : IPipe<T> {
        internal Pipe( [CanBeNull] T value ) => Value = new Option<T>(value);
        internal Pipe( Option<T> value )     => Value   = value;

        internal Option<T> Value { get; }

        internal static Pipe<T> SkipPipe => new Pipe<T>(Option<T>.None);

        internal Pipe<TOut> Transform<TOut>([NotNull] Func<T, TOut> func) {
            foreach (var value in Value)
                return start<TOut>.pipe | func(value);

            return Pipe<TOut>.SkipPipe;
        }
        
        internal Pipe<TOut> Transform<TOut>([NotNull] Func<Option<T>, Option<TOut>> func) => start<TOut>.pipe | func(Value);

        /// <summary>
        /// Transforms pipe contents using a function on the right.
        /// </summary>
        public static Pipe<T> operator |( Pipe<T> pipe, Func<T, T> func ) => pipe.Transform(func);
        
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