using System;
using System.ComponentModel;
using JetBrains.Annotations;
using Sutra.Transformations;

namespace Sutra {
    public static partial class Commands {
        /// <summary>
        /// Throws an exception if the condition on the right is met.
        /// </summary>
        /// <example><code>
        /// pipe | fail | when | isnull; 
        /// seq  | fail | whenany | equals(1);
        /// seq  | fail | new InvalidOperationException("Invalid Value") | whenany | equals(1);
        /// </code></example>
        public static DoFail   fail     => new DoFail(null);
        
        /// <summary>
        /// Throws an exception with message if the condition on the right is met.
        /// </summary>
        public static DoFailWith   failwith([CanBeNull] string message)     => new DoFailWith(message);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoFail
    {
        public DoFail( [CanBeNull] object obj ) => NextException.Reset();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoFailWith
    {
        /// <summary>
        /// This identifier will be replaced with the pipe contents.
        /// </summary>
        private const string PipeStringIdentifier = "$pipe";
        private const string SeqStringIdentifier  = "$seq";
        
        internal readonly string Message;

        internal DoFailWith( [CanBeNull] string message )
            {
                NextException.Reset();
                Message = message;
            }

        internal string GetMessageFor<T>( Pipe<T> pipe )
            {
                foreach (T value in pipe.Option.Enm)
                    return Message.Replace(PipeStringIdentifier, value.ToString());

                return Message.Replace(PipeStringIdentifier, pipe.GetType().GetFriendlyName());
            }

        internal string GetMessageFor<T>( Seq<T> seq )
            {
                return Message.Replace(SeqStringIdentifier, seq.GetType().GetFriendlyName());
            }
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoFail<T> : Command<T> {
        internal readonly Option<Exception> UserException;

        [NotNull]
        private Exception GetExceptionFor( [CanBeNull] object exceptionSource = null )
            {
                foreach (Exception exc in UserException.Enm)
                    return exc;

                foreach (Exception exc in NextException.Get().Enm)
                    return exc;

                return new SutraCommandException("fail");
            }
        
        internal void ThrowExceptionFor( [CanBeNull] object exceptionSource = null )
            {
                throw GetExceptionFor(exceptionSource);
            }

        internal DoFail( [NotNull] IPipe<T> pipe, Option<Exception> exc, string message = null ) : base(pipe) {
            if (exc.HasValue)          UserException = exc;
            else if (message != null)  UserException = new SutraUserException(message).To<Exception>("DoFail").ToOption();
        }
    }
}