using System;
using JetBrains.Annotations;

namespace SharpPipe
{
    public static class OptionMixin
    {
        internal static T _value<T>( this IOption<T> option ) => ((IOptionValue<T>) option).Value;

        [CanBeNull]
        public static U Match<T, U>( [NotNull] this IOption<T> option, [NotNull] Func<T, U> some, [NotNull] Func<U> none )
            {
                if (option == null) throw new ArgumentNullException(nameof(option));
                if (some == null) throw new ArgumentNullException(nameof(some));
                if (none == null) throw new ArgumentNullException(nameof(none));
                
                return option.HasValue ? some(option._value()) : none();
            }

        [NotNull]
        public static U Match<T, U>( [NotNull] this IOption<T> option, [NotNull] Func<T, U> some, [NotNull] U none )
            {
                return option.Match(some, () => none);
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
        /// <exception cref="EmptyOptionException"></exception>
        [NotNull]
        public static T ValueOrFail<T>( [NotNull] this IOption<T> option )
            {
                return option.ValueOrFail(EmptyOptionException.For<T>);
            }

        /// <summary>
        /// Returns the contained value or fails.
        /// </summary>
        /// <exception cref="EmptyOptionException"></exception>
        [NotNull]
        public static T ValueOrFail<T>( [CanBeNull] this IOption<T> option, string failMessage )
            {
                return option.ValueOrFail(() => new EmptyOptionException(failMessage));
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

    public static class NonGenericOptionMixin
    {
        internal static object BoxedValue( this IOption option ) => ((IOptionValue) option).BoxedValue;
    }
}