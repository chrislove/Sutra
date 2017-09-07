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
            set { _nextException = value; }
        }
    }
    
    public class DoThrow {}
    
    public partial struct EnumPipe<TOut> {
        // PIPE '|' THROW
        [NotNull] public static DoThrow<TOut> operator |( EnumPipe<TOut> pipe, DoThrow @do ) => new DoThrow<TOut>(pipe);
    }
    
    
    
    public class DoThrow<T> : DoThrow {
        internal readonly EnumPipe<T> Pipe;
        internal readonly Exception Exception;

        protected DoThrow( DoThrow<T> copyFrom ) {
            Pipe      = copyFrom.Pipe;
            Exception = copyFrom.Exception;
        }
        
        internal DoThrow( EnumPipe<T> pipe ) {
            Pipe      = pipe;
            Exception = PIPE.NextException ?? new PipeCommandException("THROW");
        }

        private DoThrow( EnumPipe<T> pipe, [NotNull] Exception exception ) {
            Pipe      = pipe;
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }


        private DoThrow( EnumPipe<T> pipe, [CanBeNull] string message ) {
            Pipe      = pipe;
            Exception = new PipeUserException(message);
        }
        
        // PIPE | THROW '|' IF
        [NotNull] public static DoThrowIf<T> operator |( DoThrow<T> doThrow, DoIf @if ) => new DoThrowIf<T>(doThrow);
        [NotNull] public static DoThrowIfAny<T> operator |( DoThrow<T> doThrow, DoIfAny @if ) => new DoThrowIfAny<T>(doThrow);
        
        [NotNull] public static DoThrow<T> operator |( DoThrow<T> doThrow, [NotNull] string message )       => new DoThrow<T>(doThrow.Pipe, message);
        [NotNull] public static DoThrow<T> operator |( DoThrow<T> doThrow, [NotNull] Exception exception )  => new DoThrow<T>(doThrow.Pipe, exception);
    }

    public class DoThrowIf<T> : DoThrow<T> {
        internal DoThrowIf(DoThrow<T> doThrow) : base(doThrow) { }
        
        public static EnumPipe<T> operator |( DoThrowIf<T> doThrowIf, [NotNull] Func<IEnumerable<T>, bool> predicate ) {
            var pipe = doThrowIf.Pipe;
            
            if ( predicate(pipe.Get) )
                throw doThrowIf.Exception;

            return pipe;
        }
    }
    
    public sealed class DoThrowIfAny<T> : DoThrowIf<T> {
        internal DoThrowIfAny(DoThrow<T> doThrow) : base(doThrow) { }
        
        public static EnumPipe<T> operator |( DoThrowIfAny<T> doThrowIf, [NotNull] Func<T, bool> predicate ) {
            var pipe = doThrowIf.Pipe;
            
            if ( pipe.Get.Any(predicate) )
                throw doThrowIf.Exception;

            return pipe;
        }
    }
}