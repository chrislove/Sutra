using System;


namespace SharpPipe {
    public struct Unit {
        /// <summary>
        /// Executes action, and returns Unit
        /// </summary>
        public static Unit operator |( Action action, Unit unit ) {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            action();
            return new Unit();
        }
    }
}