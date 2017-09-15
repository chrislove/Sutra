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

        [Pure]
        [ContractAnnotation("null => null; notnull => notnull")]
        public static IEnumerable<T> SelectNotEmpty<T>( [CanBeNull] this IEnumerable<IOption> enm )
            => enm.Cast<Option<T>>().SelectNotEmpty();
        
        [Pure]
        [ContractAnnotation("null => null; notnull => notnull")]
        public static IEnumerable<T> SelectNotEmpty<T>( [CanBeNull] this IEnumerable<Option<T>> enm ) 
            => enm?.Where(i => i.HasValue).Select(i => i.ValueOrFail());
        
        /// <summary>
        /// Returns an empty option if at least one of the sequence values is empty.
        /// </summary>
        [Pure]
        public static Option<IEnumerable<T>> Lower<T>( [CanBeNull] this IEnumerable<Option<T>> enm )
            {
                if (enm.Any(i => !i.HasValue))
                    return Option<IEnumerable<T>>.None;

                return enm.Select(v => v.ValueOrFail()).ToOption();
            }
        
        [Pure]
        public static SeqOption<T> Return<T>( [CanBeNull] this IEnumerable<Option<T>> enm )
            {
                return new SeqOption<T>(enm);
            }

        [Pure]
        public static SeqOption<T> Return<T>( this Option<IEnumerable<T>> enm )
            {
                return enm.Match(i => new SeqOption<T>(i.Return()), SeqOption<T>.None);
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