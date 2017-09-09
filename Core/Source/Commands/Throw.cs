using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Throws an exception if any object in the EnumerablePipe matches the predicate on the right.
        /// </summary>
        /// <example><code>
        /// pipe | THROW | IFANY | IS(1);
        /// pipe | THROW | "Invalid Value" | IFANY | IS(1);
        /// pipe | THROW | new InvalidOperationException("Invalid Value") | IFANY | IS(1);
        /// </code></example>
        public static DoThrow   THROW     => new DoThrow();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoThrow {}
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoThrow<T> : Command<T> {
        internal Exception Exception = PIPE.NextException ?? new PipeCommandException("THROW");

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