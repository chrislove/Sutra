using System;
using JetBrains.Annotations;

namespace SharpPipe {
    public static class OptionMixin {
        internal static T _value<T>(this IOption<T> option ) => ((IOptionValue<T>) option).Value;
        
        [CanBeNull]
        public static U Match<T, U>(this IOption<T> option,  Func<T, U> some, Func<U> none ) => option.HasValue ? some(option._value()) : none();
        
        [CanBeNull]
        public static U Match<T, U>(this IOption<T> option, Func<T, U> some, U none ) => option.HasValue ? some(option._value()) : none;

        /// <summary>
        /// Matches any value - whether valid or invalid.
        /// </summary>
        [CanBeNull]
        public static U MatchAny<T, U>(this IOption<T> option, Func<T, U> func ) => func(option._value());
        
        /// <summary>
        /// Matches any value - whether valid or invalid.
        /// </summary>
        public static void MatchAny<T>(this IOption<T> option, Action<T> act ) => act(option._value());

        [NotNull]
        public static T ValueOr<T>(this IOption<T> option, [NotNull] T alternative ) => option._value() != null && option.HasValue ? option._value() : alternative;

        [NotNull]
        public static T ValueOrFail<T>(this IOption<T> option) => option._value() != null && option.HasValue ? option._value() : throw EmptyOptionException.For<T>();

        [NotNull]
        public static T ValueOrFail<T>(this IOption<T> option, string failMessage ) => option._value() != null && option.HasValue ? option._value() : throw new EmptyOptionException(failMessage);
    }
    
    public static class NonGenericOptionMixin {
        internal static object _value(this IOption option ) => ((IOptionValue) option).BoxedValue;
        
        [CanBeNull]
        public static object Match(this IOption option,  Func<object, object> some, Func<object> none ) => option.HasValue ? some(option._value()) : none();
        
        [CanBeNull]
        public static object Match(this IOption option, Func<object, object> some, object none ) => option.HasValue ? some(option._value()) : none;

        /// <summary>
        /// Matches any value - whether valid or invalid.
        /// </summary>
        [CanBeNull]
        public static object MatchAny(this IOption option, Func<object, object> func ) => func(option._value());
        
        /// <summary>
        /// Matches any value - whether valid or invalid.
        /// </summary>
        public static void MatchAny(this IOption option, Action<object> act ) => act(option._value());

        [NotNull]
        public static object ValueOr(this IOption option, [NotNull] object alternative ) => option._value() != null && option.HasValue ? option._value() : alternative;

        [NotNull]
        public static object ValueOrFail( this IOption option ) => option._value() != null && option.HasValue
                                                                       ? option._value()
                                                                       : throw new EmptyOptionException("IOption");

        [NotNull]
        public static object ValueOrFail(this IOption option, string failMessage ) => option._value() != null && option.HasValue ? option._value() : throw new EmptyOptionException(failMessage);
    }
    
    
}