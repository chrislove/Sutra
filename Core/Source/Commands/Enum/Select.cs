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
        public static DoSelect<TOut> operator |( EnumPipe<TOut> pipe, DoSelect doSelect ) => new DoSelect<TOut>(pipe);
    }
    
    
    public struct DoSelect<T> {
        internal readonly EnumPipe<T> Pipe;

        internal DoSelect( EnumPipe<T> pipe ) => Pipe = pipe;

        public static EnumPipe<T> operator |( DoSelect<T> doSelect, [NotNull] Func<T, T> func )
            => ENUM<T>.NEW | ADD | doSelect.Pipe.Get.Select(func);
    }

    
    public partial struct SharpFunc<TIn, TOut> {
        public static EnumPipe<TOut> operator |( DoSelect<TIn> doSelect, SharpFunc<TIn, TOut> func )
            => ENUM<TOut>.NEW | ADD | doSelect.Pipe.Get.Select(func.Func);
    }
}