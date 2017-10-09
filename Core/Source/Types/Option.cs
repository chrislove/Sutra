using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SharpPipe.Transformations;

namespace SharpPipe
{
    [PublicAPI]
    public struct Option<T> : ISimpleOption<T>, IOptionValue<T>
    {
        public bool HasValue { get; }

        [CanBeNull] T IOptionValue<T>.Value => _value;
        [CanBeNull] object IOptionValue.BoxedValue => _value;

        private readonly T _value;

        public Option( [CanBeNull] T value )
            {
                _value = value;
                HasValue = value != null;

                if (value is string str)
                    HasValue = !string.IsNullOrEmpty(str);
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


        #region Boilerplate

        public bool Equals( IOption<T> other ) => HasValue == other.HasValue && EqualityComparer<T>.Default.Equals(_value, other._value());
        public bool Equals( IOption other ) => HasValue == other.HasValue && EqualityComparer<T>.Default.Equals(_value, (T) other.BoxedValue());
        public bool Equals( T other ) => HasValue == (other != null) && EqualityComparer<T>.Default.Equals(_value, other);

        public override bool Equals( object obj )
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is Option<T> && Equals((Option<T>) obj);
            }

        public override int GetHashCode()
            {
                unchecked
                    {
                        return (HasValue.GetHashCode() * 397) ^ EqualityComparer<T>.Default.GetHashCode(_value);
                    }
            }

        public static bool operator ==( Option<T> lhs, IOption rhs ) => lhs.Equals(rhs);
        public static bool operator !=( Option<T> lhs, IOption rhs ) => !(lhs == rhs);

        public static bool operator ==( Option<T> lhs, T rhs ) => lhs.Equals(rhs);
        public static bool operator !=( Option<T> lhs, T rhs ) => !(lhs == rhs);

        #endregion
    }
}