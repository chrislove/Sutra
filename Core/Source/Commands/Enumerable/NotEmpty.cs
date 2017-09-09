using System;
using System.Collections.Generic;
using System.ComponentModel;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Throws an exception if the underlying EnumerablePipe collection is empty
        /// </summary>
        public static DoNotEmpty NOTEMPTY => new DoNotEmpty();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoNotEmpty {}

    public partial struct EnumerablePipe<T> {
        public static EnumerablePipe<T> operator |( EnumerablePipe<T> pipe, DoNotEmpty notEmpty ) {
            var exception = EmptyPipeException.For<EnumerablePipe<T>>();
            return pipe | THROW | exception | IF | (Func<IEnumerable<T>, bool>) ISEMPTY;
        }
    }
}