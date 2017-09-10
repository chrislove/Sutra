using System;
using System.Collections.Generic;
using System.ComponentModel;
using static SharpPipe.Commands;



namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Throws an exception if the underlying Sequence collection is empty
        /// </summary>
        public static DoNotEmpty notempty => new DoNotEmpty();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoNotEmpty {}

    public partial struct Seq<T> {
        public static Seq<T> operator |( Seq<T> pipe, DoNotEmpty notEmpty ) {
            var exception = EmptyPipeException.For<Seq<T>>();
            return pipe | fail | exception | when | (Func<IEnumerable<T>, bool>) isempty;
        }
    }
}