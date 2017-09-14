using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace SharpPipe
{
    public partial struct Seq<T>
    {
        [NotNull]
        public static DoFailSeq<T> operator |( Seq<T> seq, DoFail _ ) => new DoFailSeq<T>(seq);

        public static DoFailSeq<T> operator |( Seq<T> seq, DoFailWith failWith ) => new DoFailSeq<T>(seq, null, failWith.Message);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoFailSeq<T> : DoFail<T>
    {
        internal DoFailSeq( [NotNull] IPipe<T> pipe, Exception exc = null, string message = null ) : base(pipe, exc, message) { }

        // PIPE | fail '|' when
        [NotNull]
        public static DoFailIfSeq<T> operator |( DoFailSeq<T> doFail, DoWhen _ )
            => new DoFailIfSeq<T>(doFail.Pipe, doFail.Exception);

        //[NotNull] public static DoFailIfAnySeq<T> operator |( DoFailSeq<T> doFail, DoWhenAny _ ) => new DoFailIfAnySeq<T>(doFail.Pipe, doFail.Exception);

        /// <summary>
        /// Attaches exception to the fail command.
        /// </summary>
        [NotNull]
        public static DoFailSeq<T> operator |( DoFailSeq<T> doFail, [NotNull] Exception exception )
            => new DoFailSeq<T>(doFail.Pipe, exception);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoFailIfSeq<T> : DoFailSeq<T>
    {
        internal DoFailIfSeq( [NotNull] IPipe<T> pipe, Exception exc = null, string message = null ) : base(pipe, exc, message) { }

        /*
        /// <summary>
        /// Throws if predicate on the right evaluates to true.
        /// </summary>
        public static Seq<T> operator |( DoFailIfSeq<T> doFailIf, [NotNull] Func<SeqOption<T>, bool> predicate ) {
            var seq = (Seq<T>)doFailIf.Pipe;

             if (predicate(seq.Option)) throw doFailIf.Exception;
            
            return seq;
        }*/

        /// <summary>
        /// Throws if predicate on the right evaluates to true.
        /// </summary>
        public static Seq<T> operator |( DoFailIfSeq<T> doFailIf, [NotNull] Func<ISeqOption, bool> predicate )
            {
                var seq = (Seq<T>) doFailIf.Pipe;

                Func<Exception> excFactory = () => predicate.TryGetException() ?? doFailIf.Exception;

                if (predicate(seq.Option)) throw excFactory();

                return seq;
            }

        /*
        /// <summary>
        /// Throws if predicate on the right evaluates to true.
        /// </summary>
        public static Seq<T> operator |( DoFailIfSeq<T> doFailIf, [NotNull] Func<IEnumerable<IOption>, bool> predicate ) {
            var seq = (Seq<T>)doFailIf.Pipe;

            foreach (var enm in seq.Option)
                if (predicate(enm.ToIOption())) throw doFailIf.Exception;

            return seq;
        }*/

        /*
        /// <summary>
        /// Throws if predicate on the right evaluates to true.
        /// </summary>
        public static Seq<T> operator |( DoFailIfSeq<T> doFailIf, [NotNull] Func<IEnumerable<T>, bool> predicate ) {
            var seq = (Seq<T>)doFailIf.Pipe;

            foreach (var enm in seq.Option.Lower())
                if (predicate(enm)) throw doFailIf.Exception;

            return seq;
        }*/

        /// <summary>
        /// Throws if predicate on the right evaluates to true.
        /// </summary>
        public static Seq<T> operator |( DoFailIfSeq<T> doFailIf, [NotNull] Func<bool> predicate )
            {
                if (predicate()) throw doFailIf.Exception;

                return (Seq<T>) doFailIf.Pipe;
            }
    }

    /*
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class DoFailIfAnySeq<T> : DoFailIfSeq<T> {
        internal DoFailIfAnySeq( [NotNull] IPipe<T> pipe, Exception exc = null, string message = null ) : base(pipe, exc, message) { }

        /// <summary>
        /// Throws if predicate on the right evaluates to true.
        /// </summary>
        public static Seq<T> operator |( DoFailIfAnySeq<T> doFailIf, [NotNull] Func<IOption, bool> predicate ) {
            var seq = (Seq<T>)doFailIf.Pipe;
            
            foreach (var value in seq.Option) {
                
            }
            
            seq.Option.MatchAny(i => {
                                 if (i.Any(predicate) ) throw doFailIf.Exception;
                             });
            return seq;
        }
        
        /// <summary>
        /// Throws if predicate on the right evaluates to true.
        /// </summary>
        public static Seq<T> operator |( DoFailIfAnySeq<T> doFailIf, [NotNull] Func<T, bool> predicate ) {
            var seq = (Seq<T>)doFailIf.Pipe;

            seq.Option.MatchAny(enm => {
                                    if (enm.Any(predicate.Map())) throw doFailIf.Exception;
                                });
            return seq;
        }
    }*/
}