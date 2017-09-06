using System;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        public static DoSelect SELECT => new DoSelect();
    }


    public struct DoSelect { }
    
    public partial struct EnumPipe<TOut> {
        public static DoSelectEnum<TOut> operator |( EnumPipe<TOut> pipe, DoSelect doSelect ) => new DoSelectEnum<TOut>(pipe);
    }
    
    public partial struct Pipe<TOut> {
        public static DoSelect<TOut> operator |( Pipe<TOut> pipe, DoSelect doSelect ) => new DoSelect<TOut>(pipe);
    }
    
    
    public struct DoSelectEnum<T> {
        internal readonly EnumPipe<T> Pipe;

        internal DoSelectEnum( EnumPipe<T> pipe ) => Pipe = pipe;

        public static EnumPipe<T> operator |( DoSelectEnum<T> doSelect, [NotNull] Func<T, T> func )
            => doSelect.Pipe.Get.Select(func) | TO<T>();
    }
    
    public struct DoSelect<T> {
        private readonly Pipe<T> Pipe;

        internal DoSelect( Pipe<T> pipe ) => Pipe = pipe;

        public static Pipe<T> operator |( DoSelect<T> doSelect, [NotNull] Func<T, T> func )
            => func(doSelect.Pipe.Get) | TO<T>();
    }

    public partial struct SharpFunc<TIn, TOut> {
        public static EnumPipe<TOut> operator |( DoSelectEnum<TIn> doSelect, SharpFunc<TIn, TOut> func )
            => doSelect.Pipe.Get.Select(func.Func) | TO<TOut>();
    }
}