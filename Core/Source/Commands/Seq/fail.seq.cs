using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    public partial struct Seq<T> {
        // PIPE '|' fail
        [NotNull]
        public static DoThrowSeq<T> operator |( Seq<T> pipe, DoThrow @do ) => new DoThrowSeq<T>(pipe);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoThrowSeq<T> : DoThrow<T> {
        public DoThrowSeq( Seq<T> pipe ) : base(pipe) {}
        public DoThrowSeq( DoThrowSeq<T> command ) : base(command) {}

        // PIPE | fail '|' when
        [NotNull] public static DoThrowIfSeq<T> operator |( DoThrowSeq<T> doThrow, DoWhen @if )
                                            => (DoThrowIfSeq<T>) new DoThrowIfSeq<T>(doThrow).WithException(doThrow.Exception);

        [NotNull] public static DoThrowIfAnySeq<T> operator |( DoThrowSeq<T> doThrow, DoWhenAny @if )
                                            => (DoThrowIfAnySeq<T>) new DoThrowIfAnySeq<T>(doThrow).WithException(doThrow.Exception);

        [NotNull]
        public static DoThrowSeq<T> operator |( DoThrowSeq<T> doThrow, [NotNull] string message )
            => (DoThrowSeq<T>) new DoThrowSeq<T>(doThrow).WithMessage(message);

        [NotNull]
        public static DoThrowSeq<T> operator |( DoThrowSeq<T> doThrow, [NotNull] Exception exception )
            => (DoThrowSeq<T>) new DoThrowSeq<T>(doThrow).WithException(exception);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoThrowIfSeq<T> : DoThrowSeq<T> {
        public DoThrowIfSeq( DoThrowSeq<T> doThrow ) : base(doThrow) { }

        public static Seq<T> operator |( DoThrowIfSeq<T> doThrowIf, [NotNull] Func<IEnumerable<T>, bool> predicate ) {
            var pipe = (Seq<T>)doThrowIf.Pipe;
            
            if ( predicate(pipe.get) )
                throw doThrowIf.Exception;

            return pipe;
        }
        
        public static Seq<T> operator |( DoThrowIfSeq<T> doThrowIf, [NotNull] Func<bool> predicate ) {
            var pipe = (Seq<T>)doThrowIf.Pipe;
            
            if ( predicate() )
                throw doThrowIf.Exception;

            return pipe;
        }
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class DoThrowIfAnySeq<T> : DoThrowIfSeq<T> {
        public DoThrowIfAnySeq( DoThrowSeq<T> doThrow ) : base(doThrow) {}

        public static Seq<T> operator |( DoThrowIfAnySeq<T> doThrowIf, [NotNull] Func<T, bool> predicate ) {
            var pipe = (Seq<T>)doThrowIf.Pipe;
            
            if ( pipe.get.Any(predicate) )
                throw doThrowIf.Exception;

            return pipe;
        }
    }
}