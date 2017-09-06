using System;
using System.Linq;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Throws an exception if any object in the EnumPipe matches the predicate on the right.
        /// Usage: THROW & IF(null)
        /// </summary>
        public static DoThrow   THROW     => new DoThrow();
    }

    
    
    public struct DoThrow {}
    
    public partial struct EnumPipe<TOut> {
        // PIPE '|' THROW
        public static DoThrow<TOut> operator |( EnumPipe<TOut> pipe, DoThrow @do ) => new DoThrow<TOut>(pipe);
    }
    
    
    
    public struct DoThrow<T> {
        internal readonly EnumPipe<T> Pipe;
        internal readonly Exception Exception;


        internal DoThrow( EnumPipe<T> pipe ) {
            Pipe = pipe;
            Exception = new PipeCommandException("THROW");
        }
        
        internal DoThrow( EnumPipe<T> pipe, [NotNull] Exception exception ) {
            Pipe = pipe;
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }


        internal DoThrow( EnumPipe<T> pipe, [CanBeNull] string message ) {
            Pipe = pipe;
            Exception = new PipeUserException(message);
        }
        
        // PIPE | THROW '|' IF
        public static DoThrowIf<T> operator |( DoThrow<T> doThrow, DoIf @if ) => new DoThrowIf<T>(doThrow);
        
        public static DoThrow<T> operator |( DoThrow<T> doThrow, [NotNull] string message )       => new DoThrow<T>(doThrow.Pipe, message);
        public static DoThrow<T> operator |( DoThrow<T> doThrow, [NotNull] Exception exception )  => new DoThrow<T>(doThrow.Pipe, exception);
    }

    public struct DoThrowIf<T> {
        private readonly DoThrow<T> _doThrow;

        internal DoThrowIf( DoThrow<T> doThrow ) => _doThrow = doThrow;

        public static EnumPipe<T> operator |( DoThrowIf<T> doThrowIf, [NotNull] Func<T, bool> predicate ) {
            var pipe = doThrowIf._doThrow.Pipe;
            
            if ( pipe.Get.Any(predicate) )
                throw doThrowIf._doThrow.Exception;

            return pipe;
        }
    }
}