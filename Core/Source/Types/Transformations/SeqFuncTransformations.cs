using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe.Transformations {
    public static class SeqFuncTransformations
    {
        [Pure] [NotNull]
        public static Func<Option<T>, SeqOption<U>> Map<T, U>( [NotNull] this Func<T, IEnumerable<U>> func )
            {
                return i => i.Map(func).Return();
            }
        
        [Pure] [NotNull]
        public static Func<SeqOption<T>, Option<U>> Map<T, U>( [NotNull] this Func<IEnumerable<Option<T>>, U> func )
            {
                return seq => seq.Reduce(i => func(i).ToOption());
            }

        
        [Pure] [NotNull]
        public static Func<SeqOption<T>, Option<U>> Map<T, U>( [NotNull] this Func<IEnumerable<IOption>, U> func )
            {
                return Map( func.Cast().InTo<Option<T>>() );
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
    }
}