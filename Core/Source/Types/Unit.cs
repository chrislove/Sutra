using System;
using JetBrains.Annotations;


namespace SharpPipe {
    public struct Unit {
        /// <summary>
        /// Executes action, and returns Unit
        /// </summary>
        public static Unit operator |( [NotNull] Action action, Unit _ ) {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            action();
            return new Unit();
        }
    }
}