using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Sutra.Transformations;
using static Sutra.Commands;

namespace Sutra
{
    [PublicAPI]
    [JsonObject(MemberSerialization.OptIn)]
    public struct Option<T> : ISimpleOption<T>, IOptionValue<T>
    {
        [JsonProperty]
        public bool HasValue { get; }

        [CanBeNull] T IOptionValue<T>.Value => _value;
        [CanBeNull] object IOptionValue.BoxedValue => _value;

        [JsonProperty]
        private readonly T _value;

        public Option( [CanBeNull] T value )
            {
                _value = value;
                HasValue = value != null;

                if (value is string str)
                    HasValue = !string.IsNullOrEmpty(str);
            }
        
        public Option<U> FlatMap<U>([NotNull] Func<T, Option<U>> func )
            {
                return Map(func).Match(i => i, none<U>());
            }
        
        public str FlatMap([NotNull] Func<T, str> func )
            {
                return Map(func).Match(i => i, str.none);
            }

        public Option<U> Map<U>( [NotNull] Func<T, U> func )
            {
                return HasValue ? func(_value).ToOption() : default;
            }

        public Option<U> Map<U>( [NotNull] Func<T, U> func, U defaultValue )
            {
                return this.Match(func, defaultValue).ToOption();
            }
        
        public IEnumerable<T> Enm => HasValue ? _value.Yield() : Enumerable.Empty<T>();
        IEnumerable<object> ISimpleOption.Enm => Enm.Cast<object>();
        
        /// <summary>
        /// If A is empty then return B
        /// </summary>
        public static Option<T> operator |( Option<T> option, Option<T> alternative ) => option.HasValue ? option : alternative;
        
        /// <summary>
        /// If A is empty then return B
        /// </summary>
        public static Option<T> operator |( Option<T> option, T alternative ) => option.HasValue ? option : alternative.ToOption();
        
        /// <summary>
        /// If A is empty then return B
        /// </summary>
        public static Option<T> operator |( Option<T> option, [NotNull] Func<Option<T>> alternativeFunc ) => option.HasValue ? option : alternativeFunc();
        
        /// <summary>
        /// If A is empty then return B
        /// </summary>
        public static Option<T> operator |( Option<T> option, [NotNull] Func<T> alternativeFunc ) => option.HasValue ? option : alternativeFunc().ToOption();

        /// <summary>
        /// Returns the value within the Option. Unsafe.
        /// </summary>
        [NotNull]
        public static T operator |( Option<T> option, DoGet _ ) => option.ValueOrFail( () => throw EmptyOptionException.For<T>() );
        
        /// <summary>
        /// Returns the value within the str or returns the alternative. Safe.
        /// </summary>
        [CanBeNull]
        public static T operator |( Option<T> option, DoGetOr<T> getOr ) => option.HasValue ? option._value : getOr._alternative;

        /// <summary>
        /// Converts Option{T} to Some{T}. Unsafe.
        /// </summary>
        public static Some<T> operator |( Option<T> option, DoToSome _ ) => new Some<T>(option);

        #region Boilerplate

        //public override string ToString() => $"Option<{typeof(T)}> [" + (HasValue ? _value.ToString() : "none") + "]";
        
        [Obsolete]
        public override string ToString() => throw new InvalidOperationException("Calling Option{T}.ToString() not allowed.");

        
        public bool Equals( IOption<T> other ) => HasValue == other.HasValue && EqualityComparer<T>.Default.Equals(_value, other._value());
        public bool Equals( IOption other ) => HasValue == other.HasValue && EqualityComparer<T>.Default.Equals(_value, (T) other.BoxedValue());
        public bool Equals( T other ) => HasValue == (other != null) && EqualityComparer<T>.Default.Equals(_value, other);

        public override bool Equals( object obj )
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is Option<T> option && Equals(option);
            }

        public override int GetHashCode()
            {
                unchecked
                    {
                        return (HasValue.GetHashCode() * 397) ^ EqualityComparer<T>.Default.GetHashCode(_value);
                    }
            }

        public static implicit operator Option<T>( T obj ) => obj.ToOption();
        
        public static bool operator ==( Option<T> lhs, IOption rhs ) => lhs.Equals(rhs);
        public static bool operator !=( Option<T> lhs, IOption rhs ) => !(lhs == rhs);

        public static bool operator ==( Option<T> lhs, T rhs ) => lhs.Equals(rhs);
        public static bool operator !=( Option<T> lhs, T rhs ) => !(lhs == rhs);

        #endregion
    }
}