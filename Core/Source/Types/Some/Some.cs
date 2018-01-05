using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using static Sutra.CallerHelper;

namespace Sutra {
    public static class Some
    {
        public static Some<T> From<T>([NotNull] T value) => new Some<T>(value);
        public static Some<T> From<T>(Option<T> option)  => new Some<T>(option);
    }


    /// <summary>
    /// Non-nullable type that is guaranteed to contain a value.
    /// </summary>
    public struct Some<T>
    {
        private Option<T> _value;
        
        [NotNull] private T Value => _value.Match(i => i, () => throw new UninitializedSomeException($"Trying to access uninitialized {ThisTypeAsString}") );

        public Some( [NotNull] T value,
                     [CanBeNull] [CallerMemberName] string memberName = null,
                     [CanBeNull] [CallerFilePath] string filePath = null,
                     [CallerLineNumber] int lineNumber = 0 )
            {
                if (value == null)
                    throw new InvalidInputException($"Trying to create {ThisTypeAsString} from a null value." + MakeCallerString(memberName, filePath, lineNumber));
                
                _value = value;
            }

        private static string ThisTypeAsString => $"Some<{typeof(T)}>";
        
        public Some( [NotNull] IOption<T> option,
                     [CanBeNull] [CallerMemberName] string memberName = null,
                     [CanBeNull] [CallerFilePath] string filePath = null,
                     [CallerLineNumber] int lineNumber = 0)
            {
                if (option == null || !option.HasValue)
                    throw new InvalidInputException($"Trying to create {ThisTypeAsString} from an empty Option." + MakeCallerString(memberName, filePath, lineNumber));

                _value = option._value();
            }
        
        [NotNull] [PublicAPI]
        public T _ => Value;

        public static implicit operator T( Some<T> some ) => some.Value;
        public static implicit operator Option<T>( Some<T> some ) => some.Value;
        
        /// <summary>
        /// Unsafe, will throw if we're trying to create Some{T} from a null value.
        /// </summary>
        public static implicit operator Some<T>( [NotNull] T value ) => new Some<T>(value);
        
        public override string ToString() => $"Some<{typeof(T)}> [{Value}]";

        public bool Equals( Some<T> other ) => _value.Equals(other._value);

        public override bool Equals( object obj )
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is Some<T> some && Equals(some);
            }

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==( Some<T> left, Some<T> right ) => left.Equals(right);

        public static bool operator !=( Some<T> left, Some<T> right ) => !left.Equals(right);

        /// <summary>
        /// Returns the value contained within Some{T}.
        /// </summary>
        [NotNull]
        public static T operator |( Some<T> a, DoGet _ ) => a._;
        
        public static Option<T> operator |( Some<T> a, DoToOption _ ) => new Option<T>(a);

    }
}