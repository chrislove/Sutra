using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace SharpPipe {
    public partial struct Pipe<T> {
        // PIPE '|' fail
        [NotNull]
        public static DoFailPipe<T> operator |( Pipe<T> pipe, DoFail _ ) => new DoFailPipe<T>(pipe);
        
        [NotNull]
        public static DoFailPipe<T> operator |( Pipe<T> pipe, DoFailWith failWith ) => new DoFailPipe<T>(pipe, null, failWith.Message);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoFailPipe<T> : DoFail<T>{
        internal DoFailPipe( [NotNull] IPipe<T> pipe, Exception exc = null, string message = null ) : base(pipe, exc, message) { }

        // PIPE | fail '|' when
        [NotNull]
        public static DoFailIfPipe<T> operator |( DoFailPipe<T> doFail, DoWhen _ )
            => new DoFailIfPipe<T>(doFail.Pipe, doFail.Exception);

        [NotNull]
        public static DoFailPipe<T> operator |( DoFailPipe<T> doFail, [NotNull] Exception exc )
            => new DoFailIfPipe<T>(doFail.Pipe, exc);
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class DoFailIfPipe<T> : DoFailPipe<T> {
        internal DoFailIfPipe( [NotNull] IPipe<T> pipe, Exception exc = null, string message = null ) : base(pipe, exc, message) { }
        
        /// <summary>
        /// Throws if predicate on the right evaluates to true.
        /// </summary>
        public static Pipe<T> operator |( DoFailIfPipe<T> doFailIf, [NotNull] Func<T, bool> predicate ) {
            var pipe = (Pipe<T>)doFailIf.Pipe;
            var pipeOut = pipe.Get;

            //if (pipeOut.ShouldSkip) return pipe;
            
            if ( predicate(pipeOut.Contents) )
                throw doFailIf.Exception;

            return pipe;
        }
        
        /// <summary>
        /// Throws if predicate on the right evaluates to true.
        /// </summary>
        public static Pipe<T> operator |( DoFailIfPipe<T> doFailIf, [NotNull] Func<bool> predicate ) {
            var pipe = (Pipe<T>)doFailIf.Pipe;
            var pipeOut = pipe.Get;

            //if (pipeOut.ShouldSkip) return pipe;
            
            if ( predicate() )
                throw doFailIf.Exception;

            return pipe;
        }
    }
}