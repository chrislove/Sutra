// ReSharper disable InconsistentNaming

using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Performs an action on every item of EnumerablePipe
        /// </summary>
        public static DoForEachStart FOREACH => new DoForEachStart();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoForEachStart { }

    public partial struct EnumerablePipe<T> {
        public static DoForEachEnd<T> operator |( EnumerablePipe<T> pipe, DoForEachStart doConvertToEnumStart )
                            => new DoForEachEnd<T>( pipe );
    }

    public struct DoForEachEnd<T> {
        private readonly EnumerablePipe<T> _pipe;

        public DoForEachEnd( EnumerablePipe<T> lhs ) => _pipe = lhs;

        public static Unit operator |( DoForEachEnd<T> lhs, [NotNull] Action<T> action ) {
            return Unit.UnitAction( lhs._pipe.Get.ForEachAction(action) );
        }
    }
}
