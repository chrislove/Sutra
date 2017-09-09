// ReSharper disable InconsistentNaming

using System;
using JetBrains.Annotations;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Performs an action on every item of EnumPipe{T}
        /// </summary>
        public static DoForEachStart FOREACH => new DoForEachStart();
    }

    public struct DoForEachStart { }

    public partial struct EnumPipe<T> {
        public static DoForEachEnd<T> operator |( EnumPipe<T> pipe, DoForEachStart doConvertToEnumStart )
                            => new DoForEachEnd<T>( pipe );
    }

    public struct DoForEachEnd<T> {
        private readonly EnumPipe<T> _pipe;

        public DoForEachEnd( EnumPipe<T> lhs ) => _pipe = lhs;

        public static VOID operator |( DoForEachEnd<T> lhs, [NotNull] Action<T> action ) {
            return VOID.VoidAction( lhs._pipe.Get.ForEachAction(action) );
        }
    }
}
