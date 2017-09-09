// ReSharper disable InconsistentNaming

using System;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    
    public static partial class Commands {
        public static DoSelect SELECT => new DoSelect();
    }


    public struct DoSelect { }
    
    public partial struct Pipe<TOut> {
        public static DoSelectPipe<TOut> operator |( Pipe<TOut> pipe, DoSelect doSelect ) => new DoSelectPipe<TOut>(pipe);
    }
    
    public struct DoSelectPipe<T> {
        private readonly Pipe<T> Pipe;

        internal DoSelectPipe( Pipe<T> pipe ) => Pipe = pipe;

        public static Pipe<T> operator |( DoSelectPipe<T> doSelectPipe, [NotNull] Func<T, T> func ) => func(doSelectPipe.Pipe.Get) | TO<T>.PIPE;
    }
    

    public partial struct SharpFunc<TIn, TOut> {
        public static EnumPipe<TOut> operator |( DoSelectEnum<TIn> doSelect, SharpFunc<TIn, TOut> func )
            => doSelect.Pipe.Get.Select(func.Func) | TO<TOut>.PIPE;
    }
}