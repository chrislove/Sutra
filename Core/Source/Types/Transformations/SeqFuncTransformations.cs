using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Sutra.Transformations {
    public static class SeqFuncTransformations
    {
        [Pure] [NotNull]
        public static Func<Option<T>, SeqOption<U>> Map<T, U>( [NotNull] this Func<T, IEnumerable<U>> func )
            {
                return option => option.Map(func).Return();
            }
        
        [Pure] [NotNull]
        public static Func<SeqOption<T>, Option<U>> Map<T, U>( [NotNull] this Func<IEnumerable<Option<T>>, U> func )
            {
                return seq => seq.Reduce(i => func(i).ToOption());
            }

        
        [Pure] [NotNull]
        public static Func<SeqOption<T>, Option<U>> Map<T, U>( [NotNull] this Func<IEnumerable<IOption>, U> func )
            {
                return func.Cast().InTo<Option<T>>().Map();
            }

        [Pure] [NotNull]
        public static Func<SeqOption<T>, U> Map<T, U>( [NotNull] this Func<IEnumerable<IOption>, U> func, [NotNull] U defaultU )
            {
                return seq => func.Map<T, U>()(seq).ValueOr(defaultU);
            }

        [Pure] [NotNull]
        public static Func<SeqOption<T>, Option<U>> Map<T, U>( [NotNull] this Func<IEnumerable<T>, U> func )
            {
                return seq => seq.Reduce(i => func(i).ToOption());
            }

        [Pure] [NotNull]
        public static Func<SeqOption<T>, U> Map<T, U>( [NotNull] this Func<IEnumerable<T>, U> func, [NotNull] U defaultU )
            {
                return seq => Map(func)(seq).ValueOr(defaultU);
            }
        
        [Pure] [NotNull]
        public static Func<IEnumerable<Option<T>>, IEnumerable<Option<U>>> Map<T, U>( [NotNull] this Func<Option<T>, Option<U>> func )
            {
                return enm => enm.Select(func);
            }
        
        [Pure] [NotNull]
        public static Func<SeqOption<T>, SeqOption<U>> Map<T, U>( [NotNull] this Func<IEnumerable<Option<T>>, IEnumerable<Option<U>>> func )
            {
                return seq => seq.Match(func, default).Return();
            }
    }
}