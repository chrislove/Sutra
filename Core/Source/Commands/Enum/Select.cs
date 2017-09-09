using System;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public partial struct EnumPipe<T> {
        public static DoSelectEnum<T> operator |( EnumPipe<T> pipe, DoSelect doSelect ) => new DoSelectEnum<T>(pipe);
    }
    
    
    public struct DoSelectEnum<T> {
        internal readonly EnumPipe<T> Pipe;

        internal DoSelectEnum( EnumPipe<T> pipe ) => Pipe = pipe;

        public static EnumPipe<T> operator |( DoSelectEnum<T> doSelect, [NotNull] Func<T, T> func )
            => doSelect.Pipe.Get.Select(func) | TO<T>.PIPE;
    }

}