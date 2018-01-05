using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Sutra.Transformations;
using static Sutra.Commands;

namespace Sutra
{
    /// <summary>
    /// Non-nullable optional string.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public struct str : IEnumerable<somestr>, IOption<string>, IOptionValue<string>
    {
        object IOptionValue.        BoxedValue => _value;
        string IOptionValue<string>.Value      => _value;

        [JsonProperty]
        private readonly string _value;

        [JsonProperty]
        [PublicAPI]
        public bool HasValue { get; }

        public str( [CanBeNull] string value )
        {
            _value   = value;
            HasValue = !string.IsNullOrEmpty(value);
        }
        
        public str( Option<string> option )
        {
            _value   = option._value();
            HasValue = option.HasValue;
        }

        public str( [CanBeNull] string value, bool hasValue )
        {
            _value   = value;
            HasValue = hasValue;
        }

        public static str NewNotNull( [CanBeNull] string value ) => new str(value, value != null);

        public static str none => default(str);

        public Option<U> Map<U>( [NotNull] Func<string, U> func ) => HasValue ? func(_value).ToOption() : none<U>();

        public str Map( [NotNull] Func<string, string>   func ) => HasValue ? func(_value) : str.none;
        public str Map( [NotNull] Func<somestr, somestr> func ) => HasValue ? func(_value) : str.none;

        public string ValueOr( [NotNull] string alternative ) => HasValue ? _value : alternative;

        /// <summary>
        /// If A is empty then return B.
        /// </summary>
        public static str operator |( str a, str b ) => a.HasValue ? a : b;

        /// <summary>
        /// If A is empty then return B.
        /// B can be an empty string to allow for: a | "";
        /// </summary>
        public static str operator |( str a, [CanBeNull] string b ) => a.HasValue ? a : new str(b, b != null);

        /// <summary>
        /// If A is empty then return B
        /// </summary>
        public static somestr operator |( str a, somestr b ) => (a.HasValue ? a : b) | some;

        /// <summary>
        /// If A is empty then return B.
        /// </summary>
        public static str operator |( str a, [NotNull] Func<str> bFunc ) => a.HasValue ? a : bFunc();

        /// <summary>
        /// If A is empty then return B.
        /// </summary>
        public static somestr operator |( str a, [NotNull] Func<somestr> bFunc ) => (a.HasValue ? a : bFunc()) | some;

        /// <summary>
        /// If A is empty then return B.
        /// B can be an empty string to allow for: a | "";
        /// </summary>
        public static str operator |( str a, [NotNull] Func<string> bFunc ) => a.HasValue ? a : str.NewNotNull(bFunc());

        /// <summary>
        /// Returns the value within the str. Unsafe.
        /// </summary>
        public static string operator |( str a, DoGet _ ) => a.ValueOrFail(() => throw EmptyOptionException.For<string>());

        // Returns a somestr or fails
        public static somestr operator |( str a, DoFailWith failWith ) =>
            a.ValueOrFail(() => throw new EmptyOptionException(failWith.Message));

        /// <summary>
        /// Returns the value within the str or returns the alternative. Safe.
        /// </summary>
        public static string operator |( str a, DoGetOr<string> getOr ) => a.HasValue ? a._value : getOr._alternative;

        public static implicit operator str( Option<string> option ) => new str(option);
        public static implicit operator str( string         value )  => new str(value);
        public static implicit operator str( somestr        value )  => new str(value);

        //public override string ToString() => this.ValueOrFail( () => throw new EmptyOptionException("The str object is empty. Calling ToString() on str is not recommended.") );

        [Obsolete]
        public override string ToString() => throw new InvalidOperationException($"Calling str.ToString() not allowed.");

        public IEnumerator<somestr> GetEnumerator()
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