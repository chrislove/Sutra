using System;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

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
        public static DoThrow<TOut> operator |( EnumPipe<TOut> pipe, DoThrow @do ) => new DoThrow<TOut>(pipe);
    }
    
    
    
    public struct DoThrow<T> {
        internal readonly EnumPipe<T> Pipe;

        public DoThrow( EnumPipe<T> pipe ) => Pipe = pipe;
        
        public static DoThrowIf<T> operator |( DoThrow<T> doThrow, DoIf @if ) => new DoThrowIf<T>(doThrow);
    }

    
    
    
    public struct DoThrowIf<T> {
        private readonly DoThrow<T> _doThrow;

        public DoThrowIf( DoThrow<T> doThrow ) => _doThrow = doThrow;

        public static EnumPipe<T> operator |( DoThrowIf<T> doThrowIf, [NotNull] Func<T, bool> predicate ) {
            var pipe = doThrowIf._doThrow.Pipe;
            
            if ( pipe.Get.Any(predicate) )
                throw new PipeCommandException("THROW");

            return pipe;
        }
    }
    
    
    /*
    public struct DoStartThrowIf {
        internal readonly Func<dynamic, bool> Predicate;

        public DoStartThrowIf( [NotNull] Func<dynamic, bool> predicate ) => Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));

        // | THROWIF & EXC<T>()
        public static DoThrowConcrete operator *( DoStartThrowIf throwIf, DoException exc ) => new DoThrowConcrete(throwIf.Predicate, exc.Exception);
    }

    public struct DoThrowException {
        [NotNull] internal readonly Exception Exception;

        internal DoThrowException( [NotNull] Exception exception ) => Exception = exception;
        
        // | THROW | EXC<T>() | IF()
        public static DoThrowConcrete operator *( DoThrowException exception, DoIf doIf ) => new DoThrowConcrete(doIf.Predicate, exception.Exception);
    }


    public struct DoThrowConcrete<T> {
        internal readonly Func<T, bool> Predicate;
        [CanBeNull] internal readonly Exception Exception;

        internal DoThrowConcrete( Func<T, bool> predicate, [CanBeNull] Exception exception = null ) {
            Predicate = predicate;
            Exception = exception;
        }
    }

    public partial struct EnumPipe<TOut> {
        /// <summary>
        /// Forward pipe operator. Throws an exception if the condition is met.
        /// </summary>
        public static EnumPipe<TOut> operator |( EnumPipe<TOut> lhs, DoThrowConcrete doThrowConcrete ) {
            if ( lhs.Get.Any(i => doThrowConcrete.Predicate(i)) ) {
                if (doThrowConcrete.Exception == null)
                    throw new PipeCommandException("THROW");

                throw doThrowConcrete.Exception;
            }

            return lhs;
        }
    }
    
    public partial struct Pipe<TOut> {
        /// <summary>
        /// Forward pipe operator. Throws an exception if the condition is met.
        /// </summary>
        public static Pipe<TOut> operator |( Pipe<TOut> lhs, DoThrowConcrete doThrowConcrete ) {
            if ( doThrowConcrete.Predicate(lhs.Get) ) {
                if (doThrowConcrete.Exception == null)
                    throw new PipeCommandException("THROW");

                throw doThrowConcrete.Exception;
            }

            return lhs;
        }
    }*/
}