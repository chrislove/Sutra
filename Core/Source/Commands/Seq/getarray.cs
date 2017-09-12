using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;


namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Converts the contents of sequence into T[] and returns.
        /// </summary>
        public static DoReturnArray getarray => new DoReturnArray();
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoReturnArray {}

    public partial struct Seq<T> {
        /// <summary>
        /// Converts pipe contents into TOut[] and returns.
        /// </summary>
        [NotNull]
        public static T[] operator |( Seq<T> seq, DoReturnArray _ ) {
            var seqOutput = seq.Get;
            
            return seqOutput.ShouldSkip ? new T[0] : seqOutput.Contents.ToArray();
        }
    }
}