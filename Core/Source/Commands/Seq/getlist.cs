using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Converts the contents of sequence into List{T} and returns. Unsafe.
        /// </summary>
        /// <exception cref="EmptySequenceException"></exception>
        public static DoReturnList getlist => new DoReturnList();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoReturnList {}

    public partial struct Seq<T> {
        /// <summary>
        /// Converts pipe contents into List{TOut} and returns. Unsafe.
        /// </summary>
        /// <exception cref="EmptySequenceException"></exception>
        [NotNull]
        public static List<T> operator |( Seq<T> seq, DoReturnList _ ) => (seq | !!!get).ToList();

    }
}