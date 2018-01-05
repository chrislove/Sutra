using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static Sutra.Commands;

namespace Sutra.Transformations
{
    public static class SeqOptionTransformations
    {
        /// <summary>
        /// Returns an empty option if at least one of the sequence values is empty.
        /// </summary>
        [Pure]
        public static Option<IEnumerable<T>> Lower<T>( [CanBeNull] this IEnumerable<Option<T>> enm )
            {
                bool isAnyEmpty = enm.Any(i => !i.HasValue);
                
                if (isAnyEmpty)
                    return none<IEnumerable<T>>();

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

        [Pure]
        public static SeqOption<T> Flatten<T>( this IEnumerable<SeqOption<T>> enm )
            {
                bool isAnyEmpty = enm.Any(i => !i.HasValue);
                
                if (isAnyEmpty)
                    return default(SeqOption<T>);

                return enm.SelectMany(seqOption => seqOption.Match(i => i, null)).Return();
            }
    }
}