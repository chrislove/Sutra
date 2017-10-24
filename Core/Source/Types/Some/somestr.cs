using System;
using JetBrains.Annotations;
using SharpPipe.Transformations;
using static SharpPipe.Commands;

namespace SharpPipe {
    /// <summary>
    /// Non-nullable string that is guaranteed to contain a value.
    /// </summary>
    public struct somestr
    {
        [NotNull]
        private readonly string _value;
        
        public somestr( [NotNull] string value )
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidInputException("Trying to create somestr from a null or empty string.");
                
                _value = value;
            }
        
        public somestr( IOption<string> str )
            {
                if (!str.HasValue)
                    throw new InvalidInputException($"Trying to create somestr from an empty str.");

                _value = str._value();
            }

        [NotNull] [PublicAPI]
        public string get => _value;
        
        public Option<U> Map<U>( [NotNull] Func<string, U> func )
            {
                return func(_value).ToOption();
            }
        
        public str Map( [NotNull] Func<string, string> func )
            {
                return func(_value);
            }

        public override string ToString() => _value;

        public static somestr From([NotNull] string value) => new somestr(value);
        public static somestr From(str value) => new somestr(value);

        public static implicit operator string( somestr str ) => str._value;
        public static implicit operator Some<string>( somestr str ) => new Some<string>(str);
        public static implicit operator somestr( Some<string> some ) => new somestr(some);

        public static somestr operator +( somestr left, [NotNull] string right )  => left + (right | some);
        public static somestr operator +( somestr left, somestr right ) => left.get + right.get | some;
        
        /// <summary>
        /// If A is empty then return B
        /// </summary>
        public static somestr operator |( str a, somestr b ) => (a.HasValue ? a : b) | some;
        
        /// <summary>
        /// If A is empty then return B
        /// </summary>
        public static somestr operator |( [CanBeNull] string a, somestr b ) => a | opt | b;

        /// <summary>
        /// Returns the string contained within the somestr.
        /// </summary>
        [NotNull]
        public static string operator |( somestr a, DoGet _ ) => a._value;

        public bool Equals( somestr other ) => string.Equals(_value, other._value);

        public override bool Equals( object obj )
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is somestr && Equals((somestr) obj);
            }

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==( somestr left, somestr right ) => left.Equals(right);

        public static bool operator !=( somestr left, somestr right ) => !left.Equals(right);
    }
}