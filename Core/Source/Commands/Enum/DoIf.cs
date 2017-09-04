using System;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Condition command constraint.
        /// </summary>
        public static DoIf IF([NotNull] Func<dynamic, bool> predicate)   => new DoIf(predicate);
    
        /// <summary>
        /// Condition command constraint - object equality.
        /// </summary>
        public static DoIf IF<T>([CanBeNull] T obj) => new DoIf(i => ((T)i).Equals(obj) );
    
        /// <summary>
        /// Condition command constraint - null comparison.
        /// </summary>
        public static DoIf IFNULL => new DoIf(i => i == null);
    }

    public struct DoIf {
        [NotNull] internal readonly Func<object, bool> Predicate;

        public DoIf( [NotNull] Func<object, bool> predicate ) => Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
    }
}