using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    /// <summary>
    /// Shortcut for Option{IEnumerable{Option{T}}}
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PublicAPI]
    public struct SeqOption<T> : ISeqOption<T>, IOptionValue<IEnumerable<Option<T>>> {
        public bool HasValue { get; }

        [CanBeNull] IEnumerable<Option<T>> IOptionValue<IEnumerable<Option<T>>>.Value => _value;
        [CanBeNull] object IOptionValue.BoxedValue => _value;

        private readonly IEnumerable<Option<T>> _value;

        public SeqOption( [CanBeNull] IEnumerable<IOption> enm ) {
            _value   = enm.Cast<Option<T>>();
            HasValue = enm != null;
        }
        
        public SeqOption( [CanBeNull] IEnumerable<Option<T>> enm ) {
            _value   = enm;
            HasValue = enm != null;
        }
        
        public SeqOption( [CanBeNull] IEnumerable<T> enm ) {
            _value   = enm.Select(i => i.ToOption());
            HasValue = enm != null;
        }

        public SeqOption<U> Map<U>( [NotNull] Func<IEnumerable<Option<T>>, IEnumerable<Option<U>>> func ) {
            foreach (var enm in this)
                return func(enm).ToSeqOption();

            return SeqOption<U>.None;
        }

        [Pure]
        public SeqOption<U> Map<U>( [NotNull] Func<IEnumerable<IOption>, IEnumerable<Option<U>>> func ) {
            foreach (var enm in this)
                return func(enm.ToIOption()).ToSeqOption();

            return SeqOption<U>.None;
        }

        public SeqOption<U> Map<U>( [NotNull] Func<IEnumerable<T>, IEnumerable<U>> func ) {
            foreach (var enm in this)
                return func( enm.Lower() ).ToSeqOption();

            return SeqOption<U>.None;
        }


        /// <summary>
        /// Folds the inner enumerable into a single option.
        /// </summary>
        public Option<U> Fold<U>( [NotNull] Func<IEnumerable<IOption>, Option<U>> func )
            {
                foreach (var enm in this)
                    return func(enm.Cast<IOption>());

                return Option<U>.None;
            }
        
        /// <summary>
        /// Folds the inner enumerable into a single option.
        /// </summary>
        public Option<U> Fold<U>( [NotNull] Func<IEnumerable<T>, Option<U>> func )
            {
                foreach (var loweredEnm in Lower())
                    return func(loweredEnm);

                return Option<U>.None;
            }



        public Option<IEnumerable<Option<T>>> ToOption()  => HasValue ? _value.ToOption() : Option<IEnumerable<Option<T>>>.None;
        public Option<IEnumerable<IOption>>   ToIOption() => HasValue ? _value.Cast<IOption>().ToOption() : Option<IEnumerable<IOption>>.None;

        
        //public SeqOption<U> Map<U>( Func<T, U> func, U defaultValue ) => this.Match(func, defaultValue).Return();

        [Pure]
        public Option<IEnumerable<T>> Lower() {
            foreach (var enm in this) {
                if (enm.Any(i => !i.HasValue))
                    return Option<IEnumerable<T>>.None;

                return enm.Select(v => v._value()).ToOption();
            }

            return Option<IEnumerable<T>>.None;
        }
        
        [NotNull]
        [Pure]
        public IEnumerable<T> UnsafeLower() => this.Lower().ValueOrFail($"{this.T()}.UnsafeLower()");


        public static SeqOption<T> None => new SeqOption<T>();

        #region Boilerplate
        public IEnumerator<IEnumerable<Option<T>>> GetEnumerator() {
            if (HasValue) yield return _value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<IEnumerable<IOption>> IEnumerable<IEnumerable<IOption>>.GetEnumerator()
            {
                if (HasValue) yield return _value.Cast<IOption>();
            }
        

        public override int GetHashCode() {
            unchecked {
                return ((_value != null ? _value.GetHashCode() : 0) * 397) ^ HasValue.GetHashCode();
            }
        }

        public bool Equals( SeqOption<T> other ) => Equals(_value, other._value) && HasValue == other.HasValue;

        public bool Equals( IOption<IEnumerable<Option<T>>> other ) {
            return Equals(_value, other._value() ) && HasValue == other.HasValue;
        }

        public override bool Equals( object obj ) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SeqOption<T> && Equals((SeqOption<T>) obj);
        }
        #endregion
    } 
}