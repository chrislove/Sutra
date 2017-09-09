using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    public partial struct EnumerablePipe<T> {
        // PIPE '|' THROW
        [NotNull]
        public static DoThrowEnum<T> operator |( EnumerablePipe<T> pipe, DoThrow @do ) => new DoThrowEnum<T>(pipe);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoThrowEnum<T> : DoThrow<T> {
        public DoThrowEnum( EnumerablePipe<T> pipe ) : base(pipe) {}
        public DoThrowEnum( DoThrowEnum<T> command ) : base(command) {}

        // PIPE | THROW '|' IF
        [NotNull] public static DoThrowIfEnum<T> operator |( DoThrowEnum<T> doThrow, DoIf @if )
                                            => (DoThrowIfEnum<T>) new DoThrowIfEnum<T>(doThrow).WithException(doThrow.Exception);

        [NotNull] public static DoThrowIfAnyEnum<T> operator |( DoThrowEnum<T> doThrow, DoIfAny @if )
                                            => (DoThrowIfAnyEnum<T>) new DoThrowIfAnyEnum<T>(doThrow).WithException(doThrow.Exception);

        [NotNull]
        public static DoThrowEnum<T> operator |( DoThrowEnum<T> doThrow, [NotNull] string message )
            => (DoThrowEnum<T>) new DoThrowEnum<T>(doThrow).WithMessage(message);

        [NotNull]
        public static DoThrowEnum<T> operator |( DoThrowEnum<T> doThrow, [NotNull] Exception exception )
            => (DoThrowEnum<T>) new DoThrowEnum<T>(doThrow).WithException(exception);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoThrowIfEnum<T> : DoThrowEnum<T> {
        public DoThrowIfEnum( DoThrowEnum<T> doThrow ) : base(doThrow) { }

        public static EnumerablePipe<T> operator |( DoThrowIfEnum<T> doThrowIf, [NotNull] Func<IEnumerable<T>, bool> predicate ) {
            var pipe = (EnumerablePipe<T>)doThrowIf.Pipe;
            
            if ( predicate(pipe.Get) )
                throw doThrowIf.Exception;

            return pipe;
        }
        
        public static EnumerablePipe<T> operator |( DoThrowIfEnum<T> doThrowIf, [NotNull] Func<bool> predicate ) {
            var pipe = (EnumerablePipe<T>)doThrowIf.Pipe;
            
            if ( predicate() )
                throw doThrowIf.Exception;

            return pipe;
        }
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class DoThrowIfAnyEnum<T> : DoThrowIfEnum<T> {
        public DoThrowIfAnyEnum( DoThrowEnum<T> doThrow ) : base(doThrow) {}

        public static EnumerablePipe<T> operator |( DoThrowIfAnyEnum<T> doThrowIf, [NotNull] Func<T, bool> predicate ) {
            var pipe = (EnumerablePipe<T>)doThrowIf.Pipe;
            
            if ( pipe.Get.Any(predicate) )
                throw doThrowIf.Exception;

            return pipe;
        }
    }
}