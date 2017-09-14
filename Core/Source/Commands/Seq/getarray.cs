using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Converts the contents of sequence into T[] and returns. Unsafe.
        /// </summary>
        /// <exception cref="EmptySequenceException"></exception>
        public static DoReturnArray getarray => new DoReturnArray();
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoReturnArray {}

    public partial struct Seq<T> {
        /// <summary>
        /// Converts pipe contents into TOut[] and returns. Unsafe.
        /// </summary>
        /// <exception cref="EmptySequenceException"></exception>
        [NotNull]
        public static T[] operator |( Seq<T> seq, DoReturnArray _ ) => (seq | !!!get).ToArray();
    }
}