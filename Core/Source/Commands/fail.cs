using System;
using System.ComponentModel;
using JetBrains.Annotations;


namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Throws an exception if the condition on the right is met.
        /// </summary>
        /// <example><code>
        /// pipe | fail | IFANY | IS(1);
        /// pipe | fail | "Invalid Value" | IFANY | IS(1);
        /// pipe | fail | new InvalidOperationException("Invalid Value") | IFANY | IS(1);
        /// </code></example>
        public static DoThrow   fail     => new DoThrow();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoThrow {}
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoThrow<T> : Command<T> {
        internal Exception Exception = SharpPipe.Pipe.NextException ?? new PipeCommandException("fail");

        internal  DoThrow( IPipe<T> pipe ) : base(pipe) {}
        protected DoThrow( DoThrow<T> command ) : base(command) {}

        [NotNull]
        internal DoThrow<T> WithException( [NotNull] Exception exception ) {
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));

            return this;
        }

        [NotNull]
        internal DoThrow<T> WithMessage( [CanBeNull] string message ) {
            Exception = new PipeUserException(message);

            return this;
        }
    }
}