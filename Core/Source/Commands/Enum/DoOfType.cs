using System;
using JetBrains.Annotations;

namespace SharpPipe {
    public static partial class Pipe {
        /// <summary>
        /// Specifies the type to be used for a command.
        /// </summary>
        public static DoOfType OF<T>() => new DoOfType(typeof(T));
    }
    
    public struct DoOfType {
        [NotNull] internal readonly Type Type;

        public DoOfType( [NotNull] Type type ) => Type = type ?? throw new ArgumentNullException(nameof(type));
    }
}