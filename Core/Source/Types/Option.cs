using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
    [PublicAPI]
    public struct Option<T> : IEnumerable<T>, IEquatable<Option<T>> {
        public readonly bool HasValue;
        [CanBeNull] private readonly T Value;
        
        public Option( [CanBeNull] T value ) {
            Value    = value;
            HasValue = value != null;
        }

        [CanBeNull]
        public U Match<U>( Func<T, U> some, Func<U> none ) => HasValue ? some(Value) : none();
        
        [CanBeNull]
        public U Match<U>( Func<T, U> some, U none ) => HasValue ? some(Value) : none;

        [NotNull]
        public T ValueOr( [NotNull] T alternative ) => Value != null && HasValue ? Value : alternative;

        [CanBeNull] public T ValueOrDefault => ValueOr(default(T));
        
        [NotNull]
        public T ValueOrFail() => Value != null && HasValue ? Value : throw EmptyOptionException.For<T>();
        
        public static Option<T> None => new Option<T>();

        #region Boilerplate
        public IEnumerator<T> GetEnumerator() {
            if (HasValue)
                yield return Value;
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Equals( Option<T> other ) => HasValue == other.HasValue  && EqualityComparer<T>.Default.Equals(Value, other.Value);
        public bool Equals( T other )         => HasValue == (other != null) && EqualityComparer<T>.Default.Equals(Value, other);

        public override bool Equals( object obj ) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Option<T> && Equals((Option<T>) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (HasValue.GetHashCode() * 397) ^ EqualityComparer<T>.Default.GetHashCode(Value);
            }
        }

        public static bool operator ==( Option<T> lhs, Option<T> rhs ) => lhs.Equals(rhs);
        public static bool operator !=( Option<T> lhs, Option<T> rhs ) => !(lhs == rhs);
        #endregion
    } 
}