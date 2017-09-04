using System;
using System.Linq;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Pipe {
        /// <summary>
        /// Throws an exception if any object in the EnumPipe matches the predicate on the right.
        /// Usage: THROW & IF(null)
        /// </summary>
        public static DoStartThrow   THROW     => new DoStartThrow();
        
        /// <summary>
        /// Throws an exception if any object in the EnumPipe matches the predicate.
        /// Usage: THROWIF(i => i == "A")
        /// </summary>
        public static DoStartThrowIf THROWIF([NotNull] Func<dynamic, bool> predicate) => new DoStartThrowIf(predicate);
        
        /// <summary>
        /// Throws an exception if any object in the EnumPipe is equal to the input object.
        /// Usage: THROWIF("A")
        /// </summary>
        public static DoStartThrowIf THROWIF<T>([NotNull] T obj) => new DoStartThrowIf(i => ((T)i).Equals(obj) );
    }

    public struct DoStartThrow {
        // | THROW & IF()
        public static DoThrow operator &( DoStartThrow lhs, DoIf doIf ) => new DoThrow(doIf.Predicate);
        
        // | THROW & EXC<T>() & IF()
        public static DoThrowException operator &( DoStartThrow lhs, DoException exc ) => new DoThrowException(exc.Exception);
    }
    
    public struct DoStartThrowIf {
        internal readonly Func<dynamic, bool> Predicate;

        public DoStartThrowIf( [NotNull] Func<dynamic, bool> predicate ) => Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));

        // | THROWIF & EXC<T>()
        public static DoThrow operator &( DoStartThrowIf throwIf, DoException exc ) => new DoThrow(throwIf.Predicate, exc.Exception);
    }

    public struct DoThrowException {
        [NotNull] internal readonly Exception Exception;

        internal DoThrowException( [NotNull] Exception exception ) => Exception = exception;
        
        // | THROW | EXC<T>() | IF()
        public static DoThrow operator &( DoThrowException exception, DoIf doIf ) => new DoThrow(doIf.Predicate, exception.Exception);
    }


    public struct DoThrow {
        internal readonly Func<dynamic, bool> Predicate;
        [CanBeNull] internal readonly Exception Exception;

        internal DoThrow( Func<dynamic, bool> predicate, [CanBeNull] Exception exception = null ) {
            Predicate = predicate;
            Exception = exception;
        }
    }

    public partial struct EnumPipe<TOut> {
        /// <summary>
        /// Forward pipe operator. Throws an exception if the condition is met.
        /// </summary>
        public static EnumPipe<TOut> operator |( EnumPipe<TOut> lhs, DoThrow doThrow ) {
            var lhsGet = lhs.Get;
            
            if (lhsGet == null)
                throw EmptyPipeException.ForType<EnumPipe<TOut>>("THROW");
            
            if ( lhsGet.Any(i => doThrow.Predicate(i)) ) {
                if (doThrow.Exception == null)
                    throw new PipeCommandException("THROW");

                throw doThrow.Exception;
            }

            return lhs;
        }
    }
}