using System;
using System.ComponentModel;
using JetBrains.Annotations;
using Sutra.Transformations;

namespace Sutra
{
    public partial struct Seq<T>
    {
        [NotNull]
        public static DoFailSeq<T> operator |( Seq<T> seq, DoFail _ ) => new DoFailSeq<T>(seq);

        public static DoFailSeq<T> operator |( Seq<T> seq, DoFailWith failWith ) => new DoFailSeq<T>(seq, default, failWith.GetMessageFor(seq));
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoFailSeq<T> : DoFail<T>
    {
        internal DoFailSeq( [NotNull] IPipe<T> pipe, Option<Exception> exc = default, string message = null ) : base(pipe, exc, message) { }

        // seq | fail '|' when
        [NotNull]
        public static DoFailIfSeq<T> operator |( DoFailSeq<T> doFail, DoWhen _ )
            => new DoFailIfSeq<T>(doFail.Pipe, doFail.UserException);

        /// <summary>
        /// Attaches exception to the fail command.
        /// </summary>
        [NotNull]
        public static DoFailSeq<T> operator |( DoFailSeq<T> doFail, [NotNull] Exception exception )
            => new DoFailSeq<T>(doFail.Pipe, exception.ToOption());
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoFailIfSeq<T> : DoFailSeq<T>
    {
        internal DoFailIfSeq( [NotNull] IPipe<T> pipe, Option<Exception> exc, string message = null ) : base(pipe, exc, message) { }

        /// <summary>
        /// Throws if predicate on the right evaluates to true.
        /// </summary>
        public static Seq<T> operator |( DoFailIfSeq<T> doFailIf, [NotNull] Func<ISeqOption, bool> predicate )
            {
                Seq<T> seq = doFailIf.Pipe.ToSeq();

                 if (predicate(seq.Option))
                     doFailIf.ThrowExceptionFor(predicate);

                return seq;
            }

        /// <summary>
        /// Throws if predicate on the right evaluates to true.
        /// </summary>
        public static Seq<T> operator |( DoFailIfSeq<T> doFailIf, [NotNull] Func<bool> predicate )
            {
                return doFailIf | predicate.Cast().InTo<ISeqOption>();
            }
    }
}