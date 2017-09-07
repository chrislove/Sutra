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


    public static partial class PIPE {
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
    
    public class DoThrow<T> : DoThrow {
        internal readonly IPipe<T> Pipe;
        internal readonly Exception Exception;

        protected DoThrow( DoThrow<T> copyFrom ) {
            Pipe      = copyFrom.Pipe;
            Exception = copyFrom.Exception;
        }
        
        internal DoThrow( IPipe<T> pipe ) {
            Pipe      = pipe;
            Exception = PIPE.NextException ?? new PipeCommandException("THROW");
        }

        protected DoThrow( IPipe<T> pipe, [NotNull] Exception exception ) {
            Pipe      = pipe;
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }


        protected DoThrow( IPipe<T> pipe, [CanBeNull] string message ) {
            Pipe      = pipe;
            Exception = new PipeUserException(message);
        }
    }
}