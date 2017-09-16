using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe.Transformations
{
    public static class SeqOptionTransformations
    {
        /// <summary>
        /// Lifts a value to Option{T}
        /// </summary>
        [Pure]
        public static SeqOption<T> Map<T>( [CanBeNull] this IEnumerable<Option<T>> enm )
            {
                return new SeqOption<T>(enm);
            }

        /// <summary>
        /// Lifts a value to Option{T}
        /// </summary>
        [Pure]
        public static SeqOption<T> Map<T>( [CanBeNull] this IEnumerable<T> enm )
            {
                return new SeqOption<T>(enm);
            }

        [Pure] [CanBeNull]
        public static IEnumerable<IOption> ToIOption<T>( [CanBeNull] this IEnumerable<Option<T>> enm )
            {
                return enm?.Cast<IOption>();
            }
        
        /// <summary>
        /// Returns an empty option if at least one of the sequence values is empty.
        /// </summary>
        [Pure]
        public static Option<IEnumerable<T>> Lower<T>( [CanBeNull] this IEnumerable<Option<T>> enm )
            {
                if (enm.Any(i => !i.HasValue))
                    return default;

                return enm.Select(option => option._value()).ToOption();
            }
        
        [Pure]
        public static SeqOption<T> Return<T>( [CanBeNull] this IEnumerable<Option<T>> enm )
            {
                return new SeqOption<T>(enm);
            }

        [Pure]
        public static SeqOption<T> Return<T>( this Option<IEnumerable<T>> enm )
            {
                return enm.Match(i => new SeqOption<T>(i), default(SeqOption<T>));
            }

        /// <summary>
        /// Lifts every value of the enumerable to Option{T}
        /// </summary>
        [Pure]
        public static SeqOption<T> DblReturn<T>( [CanBeNull] this IEnumerable<T> enm )
            {
                return enm.Return().Return();
            }
    }
}