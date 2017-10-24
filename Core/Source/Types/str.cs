using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using SharpPipe.Transformations;

namespace SharpPipe {
    /// <summary>
    /// Non-nullable string.
    /// </summary>
    public struct str : IEnumerable<string>, IOption<string>, IOptionValue<string>
    {
        object IOptionValue.BoxedValue => _value;
        string IOptionValue<string>.Value => _value;
        
        private readonly string _value;
	
        [PublicAPI] public bool HasValue { get; }

        public str( string value )
            {
                _value = value;
                HasValue = !string.IsNullOrEmpty(value);
            }
        
        public str( string value, bool hasValue )
            {
                _value = value;
                HasValue = hasValue;
            }

        public static str none => default;
        
        public Option<U> Map<U>( [NotNull] Func<string, U> func )
            {
                return HasValue ? func(_value).ToOption() : default;
            }
        
        public str Map( [NotNull] Func<string, string> func )
            {
                return HasValue ? func(_value) : default;
            }

        public string ValueOr( [NotNull] string alternative )
            {
                return HasValue ? _value : alternative;
            }

        /// <summary>
        /// If A is empty then return B.
        /// </summary>
        public static str operator |( str a, str b ) => a.HasValue ? a : b;
        
        /// <summary>
        /// If A is empty then return B.
        /// B can be an empty string to allow for: a | "";
        /// </summary>
        public static str operator |( str a, [NotNull] string b ) => a.HasValue ? a : new str(b, b != null);
        
        /// <summary>
        /// Returns the value within the str. Unsafe.
        /// </summary>
        public static string operator |( str a, DoGet _ ) => a.ValueOrFail( () => throw EmptyOptionException.For<string>() );

        /// <summary>
        /// Returns the value within the str or returns the alternative. Safe.
        /// </summary>
        public static string operator |( str a, DoGetOr<string> getOr ) => a.HasValue ? a._value : getOr._alternative;

        public static implicit operator str(string value) => new str(value);
        public static implicit operator str(somestr value) => new str(value);
        
        //public override string ToString() => this.ValueOrFail( () => throw new EmptyOptionException("The str object is empty. Calling ToString() on str is not recommended.") );

        public override string ToString() => throw new InvalidOperationException($"Calling str.ToString() not allowed.");
	
        public IEnumerator<string> GetEnumerator()
            {
                if (HasValue)
                    yield return _value;
            }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public bool Equals( IOption<string> other )
            {
                return string.Equals(_value, other._value()) && HasValue == other.HasValue;
            }

        public override bool Equals( object obj )
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is IOption<string> option && Equals(option);
            }

        public override int GetHashCode()
            {
                unchecked
                    {
                        return ((_value != null ? _value.GetHashCode() : 0) * 397) ^ HasValue.GetHashCode();
                    }
            }

        public static bool operator ==( str left, str right ) => left.Equals(right);

        public static bool operator !=( str left, str right ) => !left.Equals(right);
    }
}