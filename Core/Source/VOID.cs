using System;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public struct VOID {
        internal static VOID VoidAction( [NotNull] Action action ) {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            action();
            return new VOID();
        }
    }
}