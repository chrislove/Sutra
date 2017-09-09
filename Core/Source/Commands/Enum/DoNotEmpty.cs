using System;
using System.Collections.Generic;
using System.ComponentModel;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Equivalent to THROW | IF | ISEMPTY
        /// </summary>
        public static DoNotEmpty NOTEMPTY => new DoNotEmpty();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoNotEmpty {}

    public partial struct EnumPipe<T> {
        public static EnumPipe<T> operator |( EnumPipe<T> pipe, DoNotEmpty notEmpty ) {
            var exception = EmptyPipeException.For<EnumPipe<T>>();
            return pipe | THROW | exception | IF | (Func<IEnumerable<T>, bool>) ISEMPTY;
        }
    }
}