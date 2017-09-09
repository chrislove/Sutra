using System;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public partial struct EnumPipe<TOut> {
        public static DoSelectEnum<TOut> operator |( EnumPipe<TOut> pipe, DoSelect doSelect ) => new DoSelectEnum<TOut>(pipe);
    }
    
    
    public struct DoSelectEnum<T> {
        internal readonly EnumPipe<T> Pipe;

        internal DoSelectEnum( EnumPipe<T> pipe ) => Pipe = pipe;

        public static EnumPipe<T> operator |( DoSelectEnum<T> doSelect, [NotNull] Func<T, T> func )
            => doSelect.Pipe.Get.Select(func) | TO<T>.PIPE;
    }

}