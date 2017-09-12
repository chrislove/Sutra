using System;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public partial struct Seq<T> {
        public static DoForEach<T> operator |( Seq<T> seq, DoAct _ )
            => new DoForEach<T>( seq );
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    public struct DoForEach<T> {
        private readonly Seq<T> _pipe;

        internal DoForEach( Seq<T> seq ) => _pipe = seq;

        public static Unit operator |( DoForEach<T> doForEach, [NotNull] Action<T> action ) {
            var seqContents = doForEach._pipe.Get;

            if (seqContents.ShouldSkip) return unit;
            
            return (() => seqContents.Contents.ForEach(action)) | unit;
        }
    }
}
