using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Adds a single item to EnumerablePipe.
        /// </summary>
        public static DoAdd ADD => new DoAdd();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAdd { }

    public partial struct EnumerablePipe<T> {
        public static DoAdd<T> operator |( EnumerablePipe<T> pipe, DoAdd doAdd ) => new DoAdd<T>( pipe );
    }

    public partial struct DoToPipe<T> {
        /// <summary>
        /// Starts a enumerable pipe
        /// </summary>
        public static DoAdd<T> operator |( DoToPipe<T> pipe, DoAdd doAdd ) => new DoAdd<T>( EnumerablePipe<T>.Empty );
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAdd<T> {
        private readonly EnumerablePipe<T> _pipe;

        internal DoAdd( EnumerablePipe<T> lhs ) => _pipe = lhs;
        
        /// <summary>
        /// Pipe forward operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe
        /// </summary>
        public static EnumerablePipe<T> operator |(DoAdd<T> lhs, [NotNull] IEnumerable<T> rhs) {
            if (rhs == null) throw new ArgumentNullException(nameof(rhs));

            return lhs._pipe.Get.Concat(rhs) | TO<T>.PIPE;
        }
		
        /// <summary>
        /// Pipe forward operator, adds a new value to IEnumerable{T}.
        /// </summary>
        public static EnumerablePipe<T> operator |(DoAdd<T> lhs, [CanBeNull] T rhs) => lhs | Yield(rhs);


        /// <summary>
        /// Pipe forward operator, concatenates two IEnumerable{T} and returns a new EnumerablePipe
        /// </summary>
        public static EnumerablePipe<T> operator |( DoAdd<T> lhs, EnumerablePipe<T> rhs ) => lhs | rhs.Get;

        private static IEnumerable<T> Yield([CanBeNull] T item) {
            yield return item;
        }
    }
}