using System;
using JetBrains.Annotations;

namespace Sutra {
    /// <summary>
    /// A Func that is guaranteed to be non-null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Fun
    {
        public static Fun<T> From<T>([NotNull] Func<T> value) => new Fun<T>(value);
        public static Fun<T> From<T>(Option<Func<T>> value) => new Fun<T>(value);
        
        public static Fun<T,U> From<T,U>([NotNull] Func<T,U> value) => new Fun<T,U>(value);
        public static Fun<T,U> From<T,U>(Option<Func<T,U>> value) => new Fun<T,U>(value);
    }
    
    /// <summary>
    /// A Func that is guaranteed to be non-null.
    /// </summary>
    public struct Fun<T>
    {
        [NotNull] [PublicAPI]
        public Func<T> Func { get; }

        /// <summary>
        /// Use this operator to invoke the function.
        /// </summary>
        public T this[ Unit _ ] => Func();
        
        public Fun( [NotNull] Func<T> func )
            {
                Func = func ?? throw new InvalidInputException($"Trying to create Fun<{typeof(T)}> from a null value.");
            }
        
        public Fun( Option<Func<T>> option )
            {
                if (!option.HasValue)
                    throw new InvalidInputException($"Trying to create Fun<{typeof(T)}> from an empty Option.");

                Func = option._value();
            }

        public static implicit operator Func<T>( Fun<T> fun ) => fun.Func;
    }
    
    /// <summary>
    /// A Func that is guaranteed to be non-null.
    /// </summary>
    public struct Fun<T,U>
    {
        [NotNull] [PublicAPI]
        public Func<T,U> Func { get; }
        
        /// <summary>
        /// Use this operator to invoke the function.
        /// </summary>
        public U this[ [CanBeNull] T invalue ] => Func(invalue);
        
        public Fun( [NotNull] Func<T,U> func )
            {
                Func = func ?? throw new InvalidInputException($"Trying to create Fun<{typeof(T)}> from a null value.");
            }
        
        public Fun( Option<Func<T,U>> option )
            {
                if (!option.HasValue)
                    throw new InvalidInputException($"Trying to create Fun<{typeof(T)}> from an empty Option.");

                Func = option._value();
            }
        
        public static implicit operator Func<T,U>( Fun<T,U> fun ) => fun.Func;
        public static implicit operator Fun<T,U>( Func<T,U> func ) => new Fun<T, U>(func);
    }
}