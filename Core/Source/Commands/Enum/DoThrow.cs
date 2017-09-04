using System;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    public static partial class Pipe {
        /// <summary>
        /// Throws an exception if any object in the EnumPipe matches the predicate on the right.
        /// Usage: THROW & IF(null)
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static DoStartThrow THROW => new DoStartThrow();
    }

    public struct DoStartThrow {
        // | THROW & IF()
        public static DoThrow operator &( DoStartThrow lhs, DoIf doIf ) => new DoThrow(doIf);
        
        // | THROW & EXC<T>() & IF()
        public static DoThrowException operator &( DoStartThrow lhs, DoException exc ) => new DoThrowException(exc.Exception);
    }

    public struct DoThrowException {
        [NotNull] internal readonly Exception Exception;

        internal DoThrowException( [NotNull] Exception exception ) => Exception = exception;
        
        // | THROW | EXC<T>() | IF()
        public static DoThrow operator &( DoThrowException exception, DoIf doIf ) => new DoThrow(doIf, exception.Exception);
    }


    public struct DoThrow {
        internal readonly DoIf DoIf;
        [CanBeNull] internal readonly Exception Exception;

        internal DoThrow( DoIf doIf, [CanBeNull] Exception exception = null ) {
            DoIf = doIf;
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
            
            if ( lhsGet.Any(i => doThrow.DoIf.TrueFor(i)) ) {
                if (doThrow.Exception == null)
                    throw new PipeCommandException("THROW");

                throw doThrow.Exception;
            }

            return lhs;
        }
    }
}