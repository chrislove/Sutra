using System;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;



namespace SharpPipe {
    public partial struct Seq<T> {
        public static DoSelectSeq<T> operator |( Seq<T> pipe, DoSelect doSelect ) => new DoSelectSeq<T>(pipe);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoSelectSeq<T> {
        internal readonly Seq<T> Pipe;

        internal DoSelectSeq( Seq<T> pipe ) => Pipe = pipe;

        public static Seq<T> operator |( DoSelectSeq<T> doSelect, [NotNull] Func<T, T> func )
            => doSelect.Pipe.Get.Select(func) | to<T>.pipe;
    }

}