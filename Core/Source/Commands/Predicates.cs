using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe {
    public static partial class Commands {
        public static Unit unit => new Unit();
        
        [NotNull] public static Func<object, bool>   isnull                     => i => i == null;
        [NotNull] public static Func<object, bool>   notnull                    => i => !isnull(i);
        
        [NotNull] public static Func<IEnumerable<object>, bool>   isempty       => i => !i.Any();
        [NotNull] public static Func<IEnumerable<object>, bool>   isnotsingle   => i => i.Count() != 1;
        
        [NotNull] public static Func<T, bool>  equals<T>   ( T obj )     => i => EqualityComparer<T>.Default.Equals(obj, i);
        [NotNull] public static Func<T, bool>  notequals<T>( T obj )     => i => !equals(obj)(i);
    }
}