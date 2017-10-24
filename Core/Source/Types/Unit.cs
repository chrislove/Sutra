using System;
using JetBrains.Annotations;

namespace SharpPipe {
    public static partial class Commands
    {
        public static Unit unit => new Unit();
        public static Unit _ => new Unit();
    }

    public struct Unit {
        /// <summary>
        /// Executes action, and returns Unit
        /// </summary>
        public static Unit operator |( [NotNull] Action action, Unit _ ) {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            action();
            return new Unit();
        }

        /// <summary>
        /// Makes Unit return any value
        /// </summary>
        internal T Return<T>( T value ) => value;
    }
    
    public partial struct Pipe<T>
    {
        public static Unit operator |( Pipe<T> pipe, Unit unit ) => unit;
    }
    
    public partial struct Seq<T>
    {
        public static Unit operator |( Seq<T> pipe, Unit unit ) => unit;
    }
}