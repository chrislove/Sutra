using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Projects each element of a EnumerablePipe and flattens the resulting sequences into one sequence.
        /// </summary>
        /// <example>
        /// <code>
        /// PIPE | SELECTMANY | (i => Enumerable.Repeat(i, 3))
        /// </code>
        /// </example>
        public static DoSelectMany SELECTMANY => new DoSelectMany();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoSelectMany { }
    
    public partial struct EnumerablePipe<T> {
        public static DoSelectMany<T> operator |( EnumerablePipe<T> pipe, DoSelectMany doSelect ) => new DoSelectMany<T>(pipe);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoSelectMany<T> {
        internal readonly EnumerablePipe<T> Pipe;

        internal DoSelectMany( EnumerablePipe<T> pipe ) => Pipe = pipe;

        public static EnumerablePipe<T> operator |( DoSelectMany<T> doSelect, [NotNull] Func<T, IEnumerable<T>> func )
            => doSelect.Pipe.Get.SelectMany(func) | TO<T>.PIPE;
    }

    public partial struct PipeFunc<TIn, TOut> {
        public static EnumerablePipe<TOut> operator |( DoSelectMany<TIn> doSelect, PipeFunc<TIn, TOut> func )
            => doSelect.Pipe.Get.Select(func.Func) | TO<TOut>.PIPE;
    }
}