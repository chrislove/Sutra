using System;
using System.ComponentModel;
using JetBrains.Annotations;
using Sutra.Transformations;
using static Sutra.Commands;

namespace Sutra
{
    public partial struct Pipe<T>
    {
        // PIPE '|' fail
        [NotNull]
        public static DoFailPipe<T> operator |( Pipe<T> pipe, DoFail _ ) => new DoFailPipe<T>(pipe);

        [NotNull]
        public static DoFailPipe<T> operator |( Pipe<T> pipe, DoFailWith failWith ) => new DoFailPipe<T>(pipe, none<Exception>(), failWith.GetMessageFor(pipe));
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoFailPipe<T> : DoFail<T>
    {
        internal DoFailPipe( [NotNull] IPipe<T> pipe, Option<Exception> exc = default(Option<Exception>), string message = null ) : base(pipe, exc, message) { }

        // PIPE | fail '|' when
        [NotNull]
        public static DoFailIfPipe<T> operator |( DoFailPipe<T> doFail, DoWhen _ )
            => new DoFailIfPipe<T>(doFail.Pipe, doFail.UserException);

        [NotNull]
        public static DoFailPipe<T> operator |( DoFailPipe<T> doFail, [NotNull] Exception exc )
            => new DoFailIfPipe<T>(doFail.Pipe, exc.ToOption());
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class DoFailIfPipe<T> : DoFailPipe<T>
    {
        internal DoFailIfPipe( [NotNull] IPipe<T> pipe, Option<Exception> exc = default(Option<Exception>), string message = null ) : base(pipe, exc, message) { }

        /// <summary>
        /// Throws if predicate on the right evaluates to true.
        /// </summary>
        public static Pipe<T> operator |( DoFailIfPipe<T> doFailIf, [NotNull] Func<Option<T>, bool> predicate )
            {
                var pipe = (Pipe<T>) doFailIf.Pipe;

                if (predicate(pipe.Option))
                    doFailIf.ThrowExceptionFor(predicate);

                return pipe;
            }

        /// <summary>
        /// Throws if predicate on the right evaluates to true.
        /// </summary>
        public static Pipe<T> operator |( DoFailIfPipe<T> doFailIf, [NotNull] Func<IOption, bool> predicate )
            {
                return doFailIf | predicate.Cast().InTo<Option<T>>();
            }

        /// <summary>
        /// Throws if predicate on the right evaluates to true.
        /// </summary>
        public static Pipe<T> operator |( DoFailIfPipe<T> doFailIf, [NotNull] Func<T, bool> predicate )
            {
                return doFailIf | predicate.Map();
            }


        /// <summary>
        /// Throws if predicate on the right evaluates to true.
        /// </summary>
        public static Pipe<T> operator |( DoFailIfPipe<T> doFailIf, [NotNull] Func<bool> predicate )
            {
                return doFailIf | predicate.Cast().InTo<Option<T>>();
            }
    }
}