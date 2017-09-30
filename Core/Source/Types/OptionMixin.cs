using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe
{
    public static class OptionMixin
    {
        internal static T _value<T>( this IOption<T> option ) => ((IOptionValue<T>) option).Value;
        internal static object BoxedValue( this IOption option ) => ((IOptionValue) option).BoxedValue;

        [CanBeNull]
        public static U Match<T, U>( [NotNull] this IOption<T> option, [NotNull] Func<T, U> some, [NotNull] Func<U> none )
            {
                if (option == null) throw new ArgumentNullException(nameof(option));
                if (some == null) throw new ArgumentNullException(nameof(some));
                if (none == null) throw new ArgumentNullException(nameof(none));
                
                return option.HasValue ? some(option._value()) : none();
            }

        [CanBeNull]
        public static U Match<T, U>( [NotNull] this IOption<T> option, [NotNull] Func<T, U> some, [CanBeNull] U none )
            {
                return option.Match(some, () => none);
            }
        
        [CanBeNull]
        public static U Match<T, U>( [NotNull] this IOption<T> option, [NotNull] Func<T, U> some, [NotNull] Exception noneException )
            {
                return option.Match(some, () => throw noneException);
            }
        
        [CanBeNull]
        public static IEnumerable<Option<U>> Match<T, U>( [NotNull] this ISeqOption<T> option,
                                     [NotNull] Func<IEnumerable<Option<T>>, IEnumerable<Option<U>>> some, [CanBeNull]  IEnumerable<Option<U>> none )
            {
                if (option == null) throw new ArgumentNullException(nameof(option));
                if (some == null) throw new ArgumentNullException(nameof(some));
                
                return option.HasValue ? some(option._value()) : none;
            }

        [NotNull]
        public static T ValueOr<T>( [NotNull] this IOption<T> option, [NotNull] T alternative )
            {
                if (alternative == null) throw new ArgumentNullException(nameof(alternative));
                
                return option._value() != null && option.HasValue ? option._value() : alternative;
            }

        /// <summary>
        /// Returns the contained value or fails.
        /// </summary>
        [NotNull]
        public static T ValueOrFail<T>( [NotNull] this IOption<T> option, [NotNull] Func<Exception> exceptionFactory )
            {
                if (option == null) throw new ArgumentNullException(nameof(option));
                if (exceptionFactory == null) throw new ArgumentNullException(nameof(exceptionFactory));
                
                return option._value() != null && option.HasValue ? option._value() : throw exceptionFactory();
            }
    }
}