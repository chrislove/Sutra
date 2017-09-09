using System;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public partial struct EnumerablePipe<T> {
        public static DoSelectEnum<T> operator |( EnumerablePipe<T> pipe, DoSelect doSelect ) => new DoSelectEnum<T>(pipe);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoSelectEnum<T> {
        internal readonly EnumerablePipe<T> Pipe;

        internal DoSelectEnum( EnumerablePipe<T> pipe ) => Pipe = pipe;

        public static EnumerablePipe<T> operator |( DoSelectEnum<T> doSelect, [NotNull] Func<T, T> func )
            => doSelect.Pipe.Get.Select(func) | TO<T>.PIPE;
    }

}