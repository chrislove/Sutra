using System;
using System.Linq;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        public static DoFirst FIRST<T>(Func<T, bool> predicate) => new DoFirst(i => predicate(i.To<T>()));
    }
    
    public struct DoFirst {
        internal readonly Func<object, bool> Predicate;

        internal DoFirst( [NotNull] Func<object, bool> predicate ) => Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
    }
    
    public partial struct EnumPipe<TOut> {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static Pipe<TOut> operator -( EnumPipe<TOut> lhs, DoFirst act ) => PIPE.IN( lhs.Get.First( i => act.Predicate(i) ) );
    }
}