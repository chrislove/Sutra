// ReSharper disable InconsistentNaming

using System;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Transforms pipe contents using a function on the right.
        /// </summary>
        /// <example><code>
        /// pipe | SELECT | (i => i + 1)
        /// </code></example>
        public static DoSelect SELECT => new DoSelect();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoSelect { }
    
    /*
    public partial struct Pipe<T> {
        public static DoSelectPipe<T> operator |( Pipe<T> pipe, DoSelect doSelect ) => new DoSelectPipe<T>(pipe);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoSelectPipe<T> {
        private readonly Pipe<T> Pipe;

        internal DoSelectPipe( Pipe<T> pipe ) => Pipe = pipe;

        public static Pipe<T> operator |( DoSelectPipe<T> doSelectPipe, [NotNull] Func<T, T> func ) => func(doSelectPipe.Pipe.Get) | TO<T>.PIPE;
    }*/
    

    public partial struct PipeFunc<TIn, TOut> {
        public static EnumerablePipe<TOut> operator |( DoSelectEnum<TIn> doSelect, PipeFunc<TIn, TOut> func )
            => doSelect.Pipe.Get.Select(func.Func) | TO<TOut>.PIPE;
    }
}