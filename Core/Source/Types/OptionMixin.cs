using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using SharpPipe.Transformations;
using static SharpPipe.Commands;

namespace SharpPipe
{
    public static class OptionExtensions
    {
        [CanBeNull]
        public static U Match<U>( this str str, [NotNull] Func<somestr, U> some, [NotNull] Func<U> none )
            {
                if (some == null) throw new ArgumentNullException(nameof(some));
                if (none == null) throw new ArgumentNullException(nameof(none));
                
                return str.HasValue ? some(str | Commands.some) : none();
            }
        
        public static Unit Match( this str str, Act<somestr> some, Act none )
            {
                return str.HasValue ? some.Invoke(str | Commands.some) : none.Invoke();
            }
        
        public static Unit Match( this str str, Action<somestr> some, Action none ) => str.Match(some, none);
    }
    
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
        
        public static Unit MatchSome<T>( [NotNull] this IOption<T> option, Act<T> some )
            {
                return option.Match(some, unit);
            }
        
        public static Unit MatchSome<T>( [NotNull] this IOption<T> option, Action<T> some )
            {
                return option.Match(some.ReturnsUnit(), unit);
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
                return option.HasValue ? option._value() : alternative;
            }

        /// <summary>
        /// Returns the contained value or fails.
        /// </summary>
        [NotNull]
        public static T ValueOrFail<T>( [NotNull] this IOption<T> option, [NotNull] Func<Exception> exceptionFactory )
            {
                if (option == null) throw new ArgumentNullException(nameof(option));
                if (exceptionFactory == null) throw new ArgumentNullException(nameof(exceptionFactory));
                
                return option.HasValue ? option._value() : throw exceptionFactory();
            }
        
        [NotNull]
        [AssertionMethod]
        [PublicAPI]
        public static T ValueOrFail<T>( [CanBeNull] this IOption<T> option,
                                        [CanBeNull] string name = null,
                                        [CanBeNull] string message = null,
                                        [CanBeNull] [CallerMemberName] string memberName = null,
                                        [CanBeNull] [CallerFilePath] string filePath = null,
                                        [CallerLineNumber] int lineNumber = 0 )
            {
                if (option != null && option.HasValue)
                    return option._value();
                
                string nameStr    = name    != null ? $"Value '{name}'" : "Value";
                string messageStr = message != null ? $". {message}" : "";
                
                throw new EmptyOptionException($"{nameStr} of type '{typeof(T).FullName}' is null at {memberName}, {filePath}:{lineNumber} {messageStr}");

            }
    }
}