using System;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Performs an action on the contents of the pipe.
        /// </summary>
        public static DoIterate iter => new DoIterate();
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoIterate {}
    
    public partial struct Seq<T> {
        /// <summary>
        /// Sets up the iter command.
        /// </summary>
        public static DoIterate<T> operator |( Seq<T> seq, DoIterate _ ) => new DoIterate<T>( seq );
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    public struct DoIterate<T> {
        private readonly Seq<T> _pipe;

        internal DoIterate( Seq<T> seq ) => _pipe = seq;

        /// <summary>
        /// Performs the action on the right for each element of the sequence.
        /// </summary>
        public static Unit operator |( DoIterate<T> doIterate, [NotNull] Action<T> action ) {
            var seqContents = doIterate._pipe.Get;

            if (seqContents.ShouldSkip) return unit;
            
            return (() => seqContents.Contents.ForEach(action)) | unit;
        }
    }
}