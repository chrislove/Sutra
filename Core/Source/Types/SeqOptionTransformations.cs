using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe
{
    internal static class SeqOptionTransformations
    {
        /// <summary>
        /// Lifts a value to Option{T}
        /// </summary>
        [Pure]
        public static SeqOption<T> ToSeqOption<T>( [CanBeNull] this IEnumerable<Option<T>> enm )
            {
                return new SeqOption<T>(enm);
            }

        /// <summary>
        /// Lifts a value to Option{T}
        /// </summary>
        [Pure]
        public static SeqOption<T> ToSeqOption<T>( [CanBeNull] this IEnumerable<T> enm )
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

        [Pure] [NotNull]
        public static Func<Option<T>, SeqOption<U>> ToSeqBind<T, U>( [CanBeNull] this Func<T, IEnumerable<U>> func )
            {
                return i => i.Map(func).Return();
            }

        [Pure] [NotNull]
        public static Func<SeqOption<T>, Option<U>> ToSeqFold<T, U>( [NotNull] this Func<IEnumerable<IOption>, U> func )
            {
                return seq => seq.Fold(i => func(i).ToOption());
            }


        [Pure] [NotNull]
        public static Func<SeqOption<T>, U> ToSeqFold<T, U>( [NotNull] this Func<IEnumerable<IOption>, U> func, U defaultU )
            {
                return seq => func.ToSeqFold<T, U>()(seq).ValueOr(defaultU);
            }

        [Pure] [NotNull]
        public static Func<SeqOption<T>, Option<U>> ToSeqFold<T, U>( [NotNull] this Func<IEnumerable<T>, U> func )
            {
                return seq => seq.Fold(i => func(i).ToOption());
            }

        [Pure] [NotNull]
        public static Func<SeqOption<T>, U> ToSeqFold<T, U>( [NotNull] this Func<IEnumerable<T>, U> func, U defaultU )
            {
                return seq => func.ToSeqFold()(seq).ValueOr(defaultU);
            }
    }
}