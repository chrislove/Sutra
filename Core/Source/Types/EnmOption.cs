using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpPipe {
    /// <summary>
    /// Shortcut for Option{IEnumerable{Option{T}}}
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PublicAPI]
    public struct EnmOption<T> : IOption<IEnumerable<Option<T>>>, IOptionValue<IEnumerable<Option<T>>> {
        public bool HasValue { get; }

        [CanBeNull] IEnumerable<Option<T>> IOptionValue<IEnumerable<Option<T>>>.Value => _value;

        private readonly IEnumerable<Option<T>> _value;
        
        public EnmOption( [CanBeNull] IEnumerable<Option<T>> value ) {
            _value   = value;
            HasValue = value != null;
        }
        
        //public EnmOption<U> Map<U>( Func<IEnumerable<Option<T>>, IEnumerable<Option<U>>> func ) => HasValue ? func(_value).ToOption() : Option<U>.None;
        //public EnmOption<U> Map<U>(Func<T, U> func, U defaultValue ) => this.Match(func, defaultValue).ToOption();

        
        public static EnmOption<T> None => new EnmOption<T>();

        #region Boilerplate
        public IEnumerator<IEnumerable<Option<T>>> GetEnumerator() {
            if (HasValue) yield return _value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        

        public override int GetHashCode() {
            unchecked {
                return ((_value != null ? _value.GetHashCode() : 0) * 397) ^ HasValue.GetHashCode();
            }
        }

        public bool Equals( EnmOption<T> other ) => Equals(_value, other._value) && HasValue == other.HasValue;

        public bool Equals( IOption<IEnumerable<Option<T>>> other ) {
            return Equals(_value, other._value() ) && HasValue == other.HasValue;
        }

        public override bool Equals( object obj ) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is EnmOption<T> && Equals((EnmOption<T>) obj);
        }
        #endregion
    } 
}