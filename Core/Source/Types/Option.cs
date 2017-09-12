using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
    internal static class OptionExtensions {
        public static Option<T> ToOption<T>              ([CanBeNull] this T obj) => new Option<T>(obj);
        public static Option<IEnumerable<T>> ToOption<T> ([CanBeNull] this IEnumerable<T> enm) => new Option<IEnumerable<T>>(enm);
    }
    
    [PublicAPI]
    public struct Option<T> : IEnumerable<T>, IEquatable<Option<T>> {
        public readonly bool HasValue;
        [CanBeNull] private readonly T _value;
        
        public Option( [CanBeNull] T value ) {
            _value    = value;
            HasValue = value != null;
        }

        [CanBeNull]
        public U Match<U>( Func<T, U> some, Func<U> none ) => HasValue ? some(_value) : none();
        
        [CanBeNull]
        public U Match<U>( Func<T, U> some, U none ) => HasValue ? some(_value) : none;

        /// <summary>
        /// Matches any value value - whether valid or invalid.
        /// </summary>
        [CanBeNull]
        public U MatchAny<U>( Func<T, U> func ) => func(_value);
        
        /// <summary>
        /// Matches any value - whether valid or invalid.
        /// </summary>
        public void MatchAny( Action<T> act ) => act(_value);


        [NotNull]
        public T ValueOr( [NotNull] T alternative ) => _value != null && HasValue ? _value : alternative;

        [CanBeNull] public T ValueOrDefault => ValueOr(default(T));
        
        [NotNull]
        public T ValueOrFail() => _value != null && HasValue ? _value : throw EmptyOptionException.For<T>();
        
        public static Option<T> None => new Option<T>();
        
        #region Boilerplate
        public IEnumerator<T> GetEnumerator() {
            if (HasValue) yield return _value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public bool Equals( Option<T> other ) => HasValue == other.HasValue  && EqualityComparer<T>.Default.Equals(_value, other._value);
        public bool Equals( T other )         => HasValue == (other != null) && EqualityComparer<T>.Default.Equals(_value, other);

        public override bool Equals( object obj ) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Option<T> && Equals((Option<T>) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (HasValue.GetHashCode() * 397) ^ EqualityComparer<T>.Default.GetHashCode(_value);
            }
        }

        public static bool operator ==( Option<T> lhs, Option<T> rhs ) => lhs.Equals(rhs);
        public static bool operator !=( Option<T> lhs, Option<T> rhs ) => !(lhs == rhs);
        #endregion
    } 
}