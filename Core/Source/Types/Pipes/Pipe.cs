using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    /// <summary>
    /// A pipe monad containing a single object.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct Pipe<T> : IPipe<T> {
        internal Pipe( Option<T> value )      => Option = value;
        internal Pipe( [CanBeNull] T value )  => Option = value.ToOption();

        internal Option<T> Option { get; }

        internal static Pipe<T> SkipPipe => new Pipe<T>(Option<T>.None);

        [Pure] internal Pipe<TOut> Map<TOut>([NotNull] Func<T, TOut> func) => start<TOut>.pipe | func.Map()(Option);
        [Pure] internal Seq<TOut> Bind<TOut>([NotNull] Func<T, IEnumerable<TOut>> func) => start<TOut>.seq | func.Map()(Option);

        [Pure] internal Pipe<TOut> Map<TOut>([NotNull] Func<Option<T>, Option<TOut>> func)              => start<TOut>.pipe | func(Option);
        [Pure] internal Seq<TOut> Bind<TOut>([NotNull] Func<Option<T>, IEnumerable<Option<TOut>>> func) => start<TOut>.seq  | func(Option);

        /// <summary>
        /// Transforms pipe contents using a function on the right.
        /// </summary>
        [Pure] public static Pipe<T> operator |( Pipe<T> pipe, Func<T, T> func ) => pipe.Map(func);
        
        /// <summary>
        /// Transforms pipe contents using a function on the right.
        /// </summary>
        [Pure] public static Pipe<T> operator |( Pipe<T> pipe, Func<Option<T>, Option<T>> func ) => pipe.Map(func);

        /// <summary>
        /// Converts pipe into sequence and appends object on the right.
        /// </summary>
        [Pure] public static Seq<T> operator |( Pipe<T> pipe, T obj ) => pipe | obj.ToOption();

        /// <summary>
        /// Converts pipe into sequence and appends object on the right.
        /// </summary>
        [Pure] public static Seq<T> operator |( Pipe<T> pipe, Option<T> option ) => start<T>.seq | new [] {pipe.Option, option};
    }
}