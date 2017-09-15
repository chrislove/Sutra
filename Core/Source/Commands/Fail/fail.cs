using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Throws an exception if the condition on the right is met.
        /// </summary>
        /// <example><code>
        /// pipe | fail | when | isnull; 
        /// seq  | fail | whenany | equals(1);
        /// seq  | fail | new InvalidOperationException("Invalid Value") | whenany | equals(1);
        /// </code></example>
        public static DoFail   fail     => new DoFail();
        
        /// <summary>
        /// Throws an exception with message if the condition on the right is met.
        /// </summary>
        /// <example><code>
        /// seq  | failwith | "Invalid Value" | whenany | IS(1);
        /// </code></example>
        public static DoFailWith   failwith([CanBeNull] string message)     => new DoFailWith(message);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoFail {}

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoFailWith {
        internal readonly string Message;

        internal DoFailWith( [CanBeNull] string message ) => Message = message;
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoFail<T> : Command<T> {
        internal readonly Exception UserException;

        [NotNull]
        protected internal Exception GetExceptionFor( [CanBeNull] object exceptionSource = null )
            {
                return UserException ?? exceptionSource?.TryGetException() ?? new PipeCommandException("fail");
            }

        internal DoFail( [NotNull] IPipe<T> pipe, Exception exc = null, string message = null ) : base(pipe) {
            if (exc != null)     UserException = exc;
            else if (message != null) UserException = new PipeUserException(message);
        }
    }
}