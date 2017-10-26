using JetBrains.Annotations;

namespace SharpPipe {
    public static class Some
    {
        public static Some<T> From<T>([NotNull] T value) => new Some<T>(value);
        public static Some<T> From<T>(Option<T> option)  => new Some<T>(option);
    }


    /// <summary>
    /// Non-nullable type that is guaranteed to contain a value.
    /// </summary>
    public struct Some<T>
    {
        [NotNull]
        private readonly T _value;
        
        public Some( [NotNull] T value )
            {
                if (value == null)
                    throw new InvalidInputException($"Trying to create Some<{typeof(T)}> from a null value.");
                
                _value = value;
            }
        
        public Some( [NotNull] IOption<T> option )
            {
                if (option == null || !option.HasValue)
                    throw new InvalidInputException($"Trying to create Some<{typeof(T)}> from an empty Option.");

                _value = option._value();
            }
        
        [NotNull] [PublicAPI]
        public T _ => _value;

        public static implicit operator T( Some<T> some ) => some._value;
        public static implicit operator Option<T>( Some<T> some ) => some._value;
        
        public override string ToString() => $"Some<{typeof(T)}> [{_value}]";

        /// <summary>
        /// Returns the value contained within Some{T}.
        /// </summary>
        [NotNull]
        public static T operator |( Some<T> a, DoGet _ ) => a._;

    }
}