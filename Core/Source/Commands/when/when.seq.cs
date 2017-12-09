using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using Sutra.Transformations;

namespace Sutra
{
    public partial struct Seq<T>
    {
        [NotNull]
        public static DoWhenSeq<T> operator |( Seq<T> seq, DoWhen _ ) => new DoWhenSeq<T>(seq);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoWhenSeq<T> : Command<T>
    {
        protected DoWhenSeq( Command<T> doWhen ) : base(doWhen) { }
        protected internal DoWhenSeq( Seq<T> pipe ) : base(pipe) { }

        [NotNull]
        public static DoWhenSeqWithPredicate<T> operator |( [CanBeNull] DoWhenSeq<T> doWhen, [NotNull] Func<SeqOption<T>, bool> predicate )
            => new DoWhenSeqWithPredicate<T>(doWhen, predicate);

        [NotNull]
        public static DoWhenSeqWithPredicate<T> operator |( [CanBeNull] DoWhenSeq<T> doWhen, [NotNull] Func<IEnumerable<IOption>, bool> predicate )
            => doWhen | predicate.Map<T, bool>(false);

        [NotNull]
        public static DoWhenSeqWithPredicate<T> operator |( [CanBeNull] DoWhenSeq<T> doWhen, [NotNull] Func<IEnumerable<T>, bool> predicate )
            => doWhen | predicate.Map(false);

        [NotNull]
        public static DoWhenSeqWithPredicate<T> operator |( [CanBeNull] DoWhenSeq<T> doWhen, [NotNull] Func<bool> predicate )
            => doWhen | (( SeqOption<T> _ ) => predicate());
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoWhenSeqWithPredicate<T> : DoWhenSeq<T>
    {
        internal readonly Func<SeqOption<T>, bool> Predicate;

        internal DoWhenSeqWithPredicate( DoWhenSeq<T> doWhen, Func<SeqOption<T>, bool> predicate ) : base(doWhen) => Predicate = predicate;

        protected internal DoWhenSeqWithPredicate( DoWhenSeqWithPredicate<T> doWhen ) : base(doWhen) => Predicate = doWhen.Predicate;

        [NotNull]
        public static DoWhenSeqWithPredicateSelect<T> operator |( [CanBeNull] DoWhenSeqWithPredicate<T> doWhen, DoMap _ )
            => new DoWhenSeqWithPredicateSelect<T>(doWhen);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoWhenSeqWithPredicateSelect<T> : DoWhenSeqWithPredicate<T>
    {
        internal DoWhenSeqWithPredicateSelect( [NotNull] DoWhenSeqWithPredicate<T> doWhen ) : base(doWhen) { }

        /// <summary>
        /// Transforms every element of the sequence using function on the right.
        /// </summary>
        public static Seq<T> operator |( [NotNull] DoWhenSeqWithPredicateSelect<T> doSelectPipe, [NotNull] Func<Option<T>, Option<T>> func )
            {
                if (doSelectPipe == null) throw new ArgumentNullException(nameof(doSelectPipe));
                if (func == null) throw new ArgumentNullException(nameof(func));

                var seq = (Seq<T>) doSelectPipe.Pipe;

                return doSelectPipe.Predicate(seq.Option) ? seq.Option.Map(func.Map()) : seq;
            }

        public static Seq<T> operator |( [NotNull] DoWhenSeqWithPredicateSelect<T> doSelectPipe, [NotNull] Func<T, T> func )
            {
                return doSelectPipe | func.Map();
            }
    }
}