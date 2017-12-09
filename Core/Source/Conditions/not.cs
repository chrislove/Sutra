using System;
using JetBrains.Annotations;

namespace Sutra {
    internal static partial class Conditions {
        [PublicAPI] [NotNull]
        public static Func<T, bool> not<T>( Func<T, bool> func ) => i => !func(i);

        [PublicAPI]
        public static PipeFunc<T, bool> not<T>( PipeFunc<T, bool> func )
            => new PipeFunc<T, bool>( opt => opt.HasValue ? func[opt].Map(i => !i) : default );
    }
}