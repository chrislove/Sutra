using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Pipe {
        [NotNull]
        public static Func<IEnumerable<TIn>, IEnumerable<TOut>> SELECT<TIn, TOut>( Func<TIn, TOut> selector )
            => inenum => inenum.Select(selector);
    }
}