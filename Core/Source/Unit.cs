using System;
using System.ComponentModel;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    /// <summary>
    /// A 
    /// </summary>
    public struct Unit {
        internal static Unit UnitAction( [NotNull] Action action ) {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            action();
            return new Unit();
        }
    }
}