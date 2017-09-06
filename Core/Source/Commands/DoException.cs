using System;
using JetBrains.Annotations;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Throws an exception if any object in the EnumPipe matches the predicate on the right.
        /// Usage: THROWIF & (o => o == null)
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static DoException EXC<T>([CanBeNull] string message = null) where T : Exception => new DoException(typeof(T), message);
        public static DoException EXC([CanBeNull] string message = null) => new DoException( new PipeUserException(message) );
    }

    public struct DoException {
        [NotNull] internal readonly Exception Exception;

        internal DoException( [NotNull] Exception exception ) => Exception = exception ?? throw new ArgumentNullException(nameof(exception));

        internal DoException( [NotNull] Type exceptionType, [CanBeNull] string message = null ) {
            if (message == null)
                Exception = (Exception) Activator.CreateInstance(exceptionType);
            else
                Exception = (Exception) Activator.CreateInstance(exceptionType, message);
        }
    }
}