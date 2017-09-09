using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        public static DoSelectMany SELECTMANY => new DoSelectMany();
    }

    public struct DoSelectMany { }
    
    public partial struct EnumPipe<T> {
        public static DoSelectMany<T> operator |( EnumPipe<T> pipe, DoSelectMany doSelect ) => new DoSelectMany<T>(pipe);
    }
    
    public struct DoSelectMany<T> {
        internal readonly EnumPipe<T> Pipe;

        internal DoSelectMany( EnumPipe<T> pipe ) => Pipe = pipe;

        public static EnumPipe<T> operator |( DoSelectMany<T> doSelect, [NotNull] Func<T, IEnumerable<T>> func )
            => doSelect.Pipe.Get.SelectMany(func) | TO<T>.PIPE;
    }

    public partial struct SharpFunc<TIn, TOut> {
        public static EnumPipe<TOut> operator |( DoSelectMany<TIn> doSelect, SharpFunc<TIn, TOut> func )
            => doSelect.Pipe.Get.Select(func.Func) | TO<TOut>.PIPE;
    }
}