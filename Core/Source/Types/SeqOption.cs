using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SharpPipe.Transformations;

namespace SharpPipe
{
    /// <summary>
    /// Shortcut for Option{IEnumerable{Option{T}}}
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PublicAPI]
    public struct SeqOption<T> : ISeqOption<T>, IOptionValue<IEnumerable<Option<T>>>
    {
        public bool HasValue { get; }

        [CanBeNull] IEnumerable<Option<T>> IOptionValue<IEnumerable<Option<T>>>.Value => _value;
        [CanBeNull] object IOptionValue.BoxedValue => _value;

        private readonly IEnumerable<Option<T>> _value;

        public SeqOption( [CanBeNull] IEnumerable<IOption> enm )
            {
                _value = enm.Cast<Option<T>>();
                HasValue = enm != null;
            }

        public SeqOption( [CanBeNull] IEnumerable<Option<T>> enm )
            {
                _value = enm;
                HasValue = enm != null;
            }

        public SeqOption( [CanBeNull] IEnumerable<T> enm )
            {
                _value = enm.Select(i => i.ToOption());
                HasValue = enm != null;
            }

        public SeqOption<U> Map<U>( [NotNull] Func<IEnumerable<Option<T>>, IEnumerable<Option<U>>> func )
            {
                foreach (var enm in this.Enm)
                    return func(enm).Map();

                return default;
            }

        [Pure]
        public SeqOption<U> Map<U>( [NotNull] Func<IEnumerable<IOption>, IEnumerable<Option<U>>> func )
            {
                foreach (var enm in this.Enm)
                    return func(enm.ToIOption()).Map();

                return default;
            }

        public SeqOption<U> Map<U>( [NotNull] Func<IEnumerable<T>, IEnumerable<U>> func )
            {
                foreach (var enm in this.Enm)
                    foreach (IEnumerable<T> lowered in enm.Lower().Enm)
                        return func(lowered).Map();

                return default;
            }
        
        /// <summary>
        /// Folds the inner enumerable into a single option.
        /// </summary>
        public Option<U> Reduce<U>( [NotNull] Func<IEnumerable<Option<T>>, Option<U>> func )
            {
                foreach (var enm in this.Enm)
                    return func(enm);

                return default;
            }

        /// <summary>
        /// Folds the inner enumerable into a single option.
        /// </summary>
        public Option<U> Reduce<U>( [NotNull] Func<IEnumerable<IOption>, Option<U>> func )
            {
                Func<IEnumerable<Option<T>>, Option<U>> transformedFunc = enm => func(enm.Cast<IOption>() );
                
                return Reduce( transformedFunc );
            }

        /// <summary>
        /// Folds the inner enumerable into a single option.
        /// </summary>
        public Option<U> Reduce<U>( [NotNull] Func<IEnumerable<T>, Option<U>> func )
            {
                foreach (IEnumerable<T> loweredEnm in Lower().Enm)
                    return func(loweredEnm);

                return default;
            }

        public Option<IEnumerable<Option<T>>> ToOption() => HasValue ? _value.ToOption() : default;
        public Option<IEnumerable<IOption>> ToIOption() => HasValue ? _value.Cast<IOption>().ToOption() : default;


        //public SeqOption<U> Map<U>( Func<T, U> func, U defaultValue ) => this.Match(func, defaultValue).Return();

        /*
        [Pure]
        Option<IEnumerable<object>> ISeqOption.Lower()
            {
                foreach (var enm in Lower().Enm)
                    return enm.Cast<object>().ToOption();

                return default;
            }
*/

        /// <summary>
        /// Returns sequence contents if all are non-empty, otherwise none. Safe.
        /// </summary>
        [Pure]
        public Option<IEnumerable<T>> Lower()
            {
                foreach (var enm in this.Enm)
                    return enm.Lower();

                return default;
            }


        #region Boilerplate
        public IEnumerable<IEnumerable<Option<T>>> Enm   => HasValue ? _value.Yield() : Enumerable.Empty<IEnumerable<Option<T>>>();
        IEnumerable<IEnumerable<IOption>> ISeqOption.Enm => HasValue ? _value.Cast<IOption>().Yield() : Enumerable.Empty<IEnumerable<IOption>>();

        
        public override int GetHashCode()
            {
                unchecked
                    {
                        return ((_value != null ? _value.GetHashCode() : 0) * 397) ^ HasValue.GetHashCode();
                    }
            }

        public bool Equals( SeqOption<T> other ) => Equals(_value, other._value) && HasValue == other.HasValue;

        public bool Equals( IOption<IEnumerable<Option<T>>> other )
            {
                return Equals(_value, other._value()) && HasValue == other.HasValue;
            }

        public override bool Equals( object obj )
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is SeqOption<T> && Equals((SeqOption<T>) obj);
            }

        #endregion
    }
}