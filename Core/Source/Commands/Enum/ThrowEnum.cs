using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    public partial struct EnumPipe<TOut> {
        // PIPE '|' THROW
        [NotNull] public static DoThrowEnum<TOut> operator |( EnumPipe<TOut> pipe, DoThrow @do ) => new DoThrowEnum<TOut>(pipe);
    }
    
    public class DoThrowEnum<T> : DoThrow<T> {
        public DoThrowEnum( EnumPipe<T> pipe ) : base(pipe) { }
        protected DoThrowEnum( DoThrow<T> doThrow ) : base(doThrow) {}
        
        private DoThrowEnum( IPipe<T> doThrowPipe, string message ) : base(doThrowPipe, message) { }
        private DoThrowEnum( IPipe<T> doThrowPipe, Exception exception ) : base(doThrowPipe, exception) { }
        
        // PIPE | THROW '|' IF
        [NotNull] public static DoThrowIfEnum<T>    operator |( DoThrowEnum<T> doThrow, DoIf @if )   => new DoThrowIfEnum<T>(doThrow);
        [NotNull] public static DoThrowIfAnyEnum<T> operator |( DoThrowEnum<T> doThrow, DoIfAny @if ) => new DoThrowIfAnyEnum<T>(doThrow);
        
        [NotNull] public static DoThrowEnum<T> operator |( DoThrowEnum<T> doThrow, [NotNull] string message )       => new DoThrowEnum<T>(doThrow.Pipe, message);
        [NotNull] public static DoThrowEnum<T> operator |( DoThrowEnum<T> doThrow, [NotNull] Exception exception )  => new DoThrowEnum<T>(doThrow.Pipe, exception);
    }
    
    
    public class DoThrowIfEnum<T> : DoThrowEnum<T> {
        internal DoThrowIfEnum(DoThrow<T> doThrow) : base(doThrow) { }
        
        public static EnumPipe<T> operator |( DoThrowIfEnum<T> doThrowIf, [NotNull] Func<IEnumerable<T>, bool> predicate ) {
            var pipe = (EnumPipe<T>)doThrowIf.Pipe;
            
            if ( predicate(pipe.Get) )
                throw doThrowIf.Exception;

            return pipe;
        }
    }
    
    public sealed class DoThrowIfAnyEnum<T> : DoThrowIfEnum<T> {
        internal DoThrowIfAnyEnum(DoThrow<T> doThrow) : base(doThrow) { }
        
        public static EnumPipe<T> operator |( DoThrowIfAnyEnum<T> doThrowIf, [NotNull] Func<T, bool> predicate ) {
            var pipe = (EnumPipe<T>)doThrowIf.Pipe;
            
            if ( pipe.Get.Any(predicate) )
                throw doThrowIf.Exception;

            return pipe;
        }
    }
}