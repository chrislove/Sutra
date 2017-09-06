using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        public static DoAdd ADD => new DoAdd();
    }
    
    public struct DoAdd { }

    public partial struct EnumPipe<TOut> {
        public static DoAdd<TOut> operator |( EnumPipe<TOut> lhs, DoAdd doAdd ) => new DoAdd<TOut>( lhs );
    }

    public struct DoAdd<T> {
        private readonly EnumPipe<T> _pipe;

        public DoAdd( EnumPipe<T> lhs ) => _pipe = lhs;
        
        /// <summary>
        /// Pipe forward operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe{T}
        /// </summary>
        public static EnumPipe<T> operator |(DoAdd<T> lhs, [NotNull] IEnumerable<T> rhs) {
            if (rhs == null) throw new ArgumentNullException(nameof(rhs));

            return lhs._pipe.Get.Concat(rhs) | Commands.TO<T>.ENUM;
        }
		
        /// <summary>
        /// Pipe forward operator, adds a new value to IEnumerable{T}.
        /// </summary>
        public static EnumPipe<T> operator |(DoAdd<T> lhs, [CanBeNull] T rhs) => lhs | Yield(rhs);


        /// <summary>
        /// Pipe forward operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe{T}
        /// </summary>
        public static EnumPipe<T> operator |( DoAdd<T> lhs, EnumPipe<T> rhs ) => lhs | rhs.Get;

        private static IEnumerable<T> Yield([CanBeNull] T item) {
            yield return item;
        }
    }
}