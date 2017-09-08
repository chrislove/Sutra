using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Throws an exception if any object in the EnumPipe matches the predicate on the right.
        /// Usage: THROW & IF(null)
        /// </summary>
        [NotNull] public static DoThrow   THROW     => new DoThrow();
    }


    internal static partial class PIPE {
        [CanBeNull] private static Exception _nextException;

        [CanBeNull] public static Exception NextException {
            get {
                try {
                    return _nextException;
                } finally {
                    _nextException = null;
                }
            }
            set => _nextException = value;
        }
    }
    
    
    public class DoThrow {}
    
    public class DoThrow<T> : Command<T> {
        internal Exception Exception = PIPE.NextException ?? new PipeCommandException("THROW");

        protected DoThrow( IPipe<T> pipe ) : base(pipe) {}
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