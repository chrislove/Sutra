using System;
using System.Collections.Generic;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        [NotNull] public static Func<object, bool>   ISNULL                 => i => i == null;
        [NotNull] public static Func<object, bool>   NOTNULL                => i => !ISNULL(i);
        
        [NotNull] public static Func<T, bool>  IS<T>   ( T obj )     => i => EqualityComparer<T>.Default.Equals(obj, i);

        [NotNull] public static Func<T, bool>  ISNOT<T>( T obj )     => i => !IS(obj)(i);
    }
}