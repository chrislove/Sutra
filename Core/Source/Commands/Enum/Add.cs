using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        public static DoAdd ADD => new DoAdd();
    }
    
    public struct DoAdd { }

    public partial struct EnumPipe<TOut> {
        public static DoAdd<TOut> operator |( EnumPipe<TOut> pipe, DoAdd doAdd ) => new DoAdd<TOut>( pipe );
    }

    public partial struct DoStartPipe<T> {
        /// <summary>
        /// Starts a enumerable pipe
        /// </summary>
        public static DoAdd<T> operator |( DoStartPipe<T> pipe, DoAdd doAdd ) => new DoAdd<T>( EnumPipe<T>.Empty );
    }


    public struct DoAdd<T> {
        private readonly EnumPipe<T> _pipe;

        internal DoAdd( EnumPipe<T> lhs ) => _pipe = lhs;
        
        /// <summary>
        /// Pipe forward operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe{T}
        /// </summary>
        public static EnumPipe<T> operator |(DoAdd<T> lhs, [NotNull] IEnumerable<T> rhs) {
            if (rhs == null) throw new ArgumentNullException(nameof(rhs));

            return lhs._pipe.Get.Concat(rhs) | TO<T>.PIPE;
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