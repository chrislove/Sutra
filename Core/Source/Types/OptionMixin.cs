using System;
using JetBrains.Annotations;

namespace SharpPipe
{
    public static class OptionMixin
    {
        internal static T _value<T>( this IOption<T> option ) => ((IOptionValue<T>) option).Value;

        [CanBeNull]
        public static U Match<T, U>( this IOption<T> option, Func<T, U> some, Func<U> none ) => option.HasValue ? some(option._value()) : none();

        [CanBeNull]
        public static U Match<T, U>( this IOption<T> option, Func<T, U> some, U none ) => option.HasValue ? some(option._value()) : none;

        /// <summary>
        /// Matches any value - whether valid or invalid.
        /// </summary>
        [CanBeNull]
        public static U MatchAny<T, U>( this IOption<T> option, Func<T, U> func ) => func(option._value());

        /// <summary>
        /// Matches any value - whether valid or invalid.
        /// </summary>
        public static void MatchAny<T>( this IOption<T> option, Action<T> act ) => act(option._value());

        [NotNull]
        public static T ValueOr<T>( this IOption<T> option, [CanBeNull] T alternative ) => option._value() != null && option.HasValue ? option._value() : alternative;

        /// <summary>
        /// Returns the contained value or fails.
        /// </summary>
        /// <exception cref="EmptyOptionException"></exception>
        [NotNull]
        public static T ValueOrFail<T>( this IOption<T> option ) => option._value() != null && option.HasValue ? option._value() : throw EmptyOptionException.For<T>();

        /// <summary>
        /// Returns the contained value or fails.
        /// </summary>
        /// <exception cref="EmptyOptionException"></exception>
        [NotNull]
        public static T ValueOrFail<T>( [NotNull] this IOption<T> option, string failMessage )
            => option._value() != null && option.HasValue ? option._value() : throw new EmptyOptionException(failMessage);

        /// <summary>
        /// Returns the contained value or fails.
        /// </summary>
        /// <exception cref="EmptyOptionException"></exception>
        [NotNull]
        public static T ValueOrFail<T>( [NotNull] this IOption<T> option, Func<Exception> exceptionFactory )
            => option._value() != null && option.HasValue ? option._value() : throw exceptionFactory();
    }

    public static class NonGenericOptionMixin
    {
        internal static object BoxedValue( this IOption option ) => ((IOptionValue) option).BoxedValue;
    }
}