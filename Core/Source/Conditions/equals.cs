using System;
using JetBrains.Annotations;

namespace Sutra {
    internal static partial class Conditions
    {
        /// <summary>
        /// Evaluates to true if the value within the pipe is equal to obj.
        /// </summary>
        [PublicAPI] [NotNull]
        public static Func<IOption, bool> equals<T>( Option<T> obj ) => i => obj == i;

        /// <summary>
        /// Evaluates to true if the value within the pipe is not equal to obj.
        /// </summary>
        [PublicAPI] [NotNull]
        public static Func<IOption, bool> notequals<T>( Option<T> obj ) => i => !equals(obj)(i);

        /// <summary>
        /// Evaluates to true if the value within the pipe is equal to obj.
        /// </summary>
        [PublicAPI] [NotNull]
        public static Func<IOption, bool> equals<T>( T obj ) => i => (Option<T>) i == obj;

        /// <summary>
        /// Evaluates to true if the value within the pipe is not equal to obj.
        /// </summary>
        [PublicAPI] [NotNull]
        public static Func<IOption, bool> notequals<T>( T obj ) => i => !equals(obj)(i);
    }
}