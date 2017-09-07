using System;
using JetBrains.Annotations;

namespace SharpPipe {
    public partial struct Pipe<TOut> {
        // PIPE '|' THROW
        [NotNull] public static DoThrowPipe<TOut> operator |( Pipe<TOut> pipe, DoThrow @do ) => new DoThrowPipe<TOut>(pipe);
    }

    public class DoThrowPipe<T> : DoThrow<T> {
        public DoThrowPipe( Pipe<T> pipe ) : base(pipe) { }
        protected DoThrowPipe( DoThrow<T> doThrow ) : base(doThrow) {}

        private DoThrowPipe( IPipe<T> doThrowPipe, string message ) : base(doThrowPipe, message) { }
        private DoThrowPipe( IPipe<T> doThrowPipe, Exception exception ) : base(doThrowPipe, exception) { }

        // PIPE | THROW '|' IF
        [NotNull] public static DoThrowIfPipe<T>    operator |( DoThrowPipe<T> doThrow, DoIf @if )   => new DoThrowIfPipe<T>(doThrow);
        
                
        [NotNull] public static DoThrowPipe<T> operator |( DoThrowPipe<T> doThrow, [NotNull] string message )       => new DoThrowPipe<T>(doThrow.Pipe, message);
        [NotNull] public static DoThrowPipe<T> operator |( DoThrowPipe<T> doThrow, [NotNull] Exception exception )  => new DoThrowPipe<T>(doThrow.Pipe, exception);
    }
    
    public sealed class DoThrowIfPipe<T> : DoThrowPipe<T> {
        internal DoThrowIfPipe(DoThrow<T> doThrow) : base(doThrow) { }
        
        public static Pipe<T> operator |( DoThrowIfPipe<T> doThrowIf, [NotNull] Func<T, bool> predicate ) {
            var pipe = (Pipe<T>)doThrowIf.Pipe;
            
            if ( predicate(pipe.Get) )
                throw doThrowIf.Exception;

            return pipe;
        }
    }

}