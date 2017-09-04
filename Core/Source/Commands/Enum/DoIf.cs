using System;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
public static partial class Pipe {
        /// <summary>
        /// Condition command constraint.
        /// </summary>
        public static DoIf IF<T>([NotNull] Func<T, bool> predicate)   => new DoIf(i => predicate(i.To<T>()));
    
        /// <summary>
        /// Condition command constraint.
        /// </summary>
        public static DoIf IF([NotNull] Func<object, bool> predicate) => new DoIf(predicate);
    
        /// <summary>
        /// Condition command constraint - reference equality.
        /// </summary>
        public static DoIf IF([CanBeNull] object obj) => new DoIf(i => ReferenceEquals(i, obj) );
    
        /// <summary>
        /// Condition command constraint - object equality.
        /// </summary>
        public static DoIf IF<T>([CanBeNull] T obj) => new DoIf(i => ((T)i).Equals(obj) );
    
        /// <summary>
        /// Condition command constraint - null comparison.
        /// </summary>
        public static DoIf IFNULL() => new DoIf(i => i == null);
        
        public static DoIf IF_str([NotNull] Func<string, bool> predicate) => new DoIf(i => predicate(i.To<string>()));
        public static DoIf IF_int([NotNull] Func<int, bool> predicate)    => new DoIf(i => predicate(i.To<int>()));
        public static DoIf IF_flt([NotNull] Func<float, bool> predicate)  => new DoIf(i => predicate(i.To<float>()));
        public static DoIf IF_dbl([NotNull] Func<double, bool> predicate)  => new DoIf(i => predicate(i.To<double>()));
    }

    public struct DoIf {
        [NotNull] private readonly Func<object, bool> _predicate;

        public DoIf( [NotNull] Func<object, bool> predicate ) => _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));

        public bool TrueFor( [CanBeNull] object obj ) => _predicate(obj);
    }
}