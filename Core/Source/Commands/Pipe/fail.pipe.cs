using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace SharpPipe {
    public partial struct Pipe<T> {
        // PIPE '|' fail
        [NotNull]
        public static DoThrowPipe<T> operator |( Pipe<T> pipe, DoThrow @do ) => new DoThrowPipe<T>(pipe);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoThrowPipe<T> : DoThrow<T>{
        internal DoThrowPipe( Pipe<T> pipe ) : base(pipe) {}
        internal DoThrowPipe( DoThrowPipe<T> pipe ) : base(pipe) {}

        // PIPE | fail '|' when
        [NotNull] public static DoThrowIfPipe<T>    operator |( DoThrowPipe<T> doThrow, DoWhen @if ) 
                                            => (DoThrowIfPipe<T>) new DoThrowIfPipe<T>(doThrow).WithException(doThrow.Exception);


        [NotNull]
        public static DoThrowPipe<T> operator |( DoThrowPipe<T> doThrow, [NotNull] string message )
                                            => (DoThrowPipe<T>) new DoThrowPipe<T>(doThrow).WithMessage(message);

        [NotNull]
        public static DoThrowPipe<T> operator |( DoThrowPipe<T> doThrow, [NotNull] Exception exception )
            => (DoThrowPipe<T>) new DoThrowPipe<T>(doThrow).WithException(exception);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class DoThrowIfPipe<T> : DoThrowPipe<T> {
        internal DoThrowIfPipe( DoThrowPipe<T> doThrow ) : base(doThrow) {}

        public static Pipe<T> operator |( DoThrowIfPipe<T> doThrowIf, [NotNull] Func<T, bool> predicate ) {
            var pipe = (Pipe<T>)doThrowIf.Pipe;
            
            if ( predicate(pipe.get) )
                throw doThrowIf.Exception;

            return pipe;
        }
        
        public static Pipe<T> operator |( DoThrowIfPipe<T> doThrowIf, [NotNull] Func<bool> predicate ) {
            var pipe = (Pipe<T>)doThrowIf.Pipe;
            
            if ( predicate() )
                throw doThrowIf.Exception;

            return pipe;
        }
    }

}