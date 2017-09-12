using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public partial struct Seq<T> {
        [NotNull]
        public static DoWhenSeq<T> operator |( Seq<T> seq, DoWhen _ ) => new DoWhenSeq<T>(seq);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoWhenSeq<T> : Command<T> {
        protected DoWhenSeq( Command<T> doWhen ) : base(doWhen) {}
        protected internal DoWhenSeq( Seq<T> pipe ) : base(pipe) {}

        [NotNull]
        public static DoWhenSeqWithPredicate<T> operator |( [CanBeNull] DoWhenSeq<T> doWhen, [NotNull] Func<IEnumerable<T>, bool> predicate )
            => new DoWhenSeqWithPredicate<T>(doWhen, predicate);

        [NotNull]
        public static DoWhenSeqWithPredicate<T> operator |( [CanBeNull] DoWhenSeq<T> doWhen, [NotNull] Func<bool> predicate )
            => new DoWhenSeqWithPredicate<T>(doWhen, predicate);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoWhenSeqWithPredicate<T> : DoWhenSeq<T> {
        internal readonly Func<IEnumerable<T>, bool> Predicate;

        internal DoWhenSeqWithPredicate( DoWhenSeq<T> doWhen, Func<IEnumerable<T>, bool> predicate ) : base(doWhen) => Predicate = predicate;
        internal DoWhenSeqWithPredicate( DoWhenSeq<T> doWhen, Func<bool> predicate )                 : base(doWhen) => Predicate = _ => predicate();
        protected internal DoWhenSeqWithPredicate( DoWhenSeqWithPredicate<T> doWhen )                         : base(doWhen) => Predicate = doWhen.Predicate;

        [NotNull]
        public static DoWhenSeqWithPredicateSelect<T> operator |( [CanBeNull] DoWhenSeqWithPredicate<T> doWhen, DoMap _ )
            => new DoWhenSeqWithPredicateSelect<T>(doWhen);
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoWhenSeqWithPredicateSelect<T> : DoWhenSeqWithPredicate<T> {
        internal DoWhenSeqWithPredicateSelect( [NotNull] DoWhenSeqWithPredicate<T> doWhen ) : base(doWhen) {}
        
        public static Seq<T> operator |( [NotNull] DoWhenSeqWithPredicateSelect<T> doSelectPipe, [NotNull] Func<T, T> func ) {
            if (doSelectPipe == null) throw new ArgumentNullException(nameof(doSelectPipe));
            if (func == null) throw new ArgumentNullException(nameof(func));

            var seq = (Seq<T>) doSelectPipe.Pipe;
            
            foreach (var value in seq.Option) {
                // ReSharper disable PossibleMultipleEnumeration
                if (doSelectPipe.Predicate(value))
                    return start<T>.seq | value.Select(func);
                // ReSharper restore PossibleMultipleEnumeration
            }
            
            return seq;
        }
    }
}