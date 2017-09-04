using System;
using System.Linq;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Pipe {
        public static DoFilter FILTER<T>(Func<T, bool> predicate) => FILTER(i => predicate(i.To<T>()));
        public static DoFilter FILTER(Func<dynamic, bool> predicate) => new DoFilter(predicate);
    }
    
    public struct DoFilter {
        internal readonly Func<object, bool> Predicate;

        internal DoFilter( [NotNull] Func<object, bool> predicate ) => Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
    }
    
    public partial struct EnumPipe<TOut> {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static EnumPipe<TOut> operator |( EnumPipe<TOut> lhs, DoFilter filter ) {
            var filtered = lhs.Get.Where(i => filter.Predicate(i));

            return ENUM.IN(filtered);
        }
    }
}