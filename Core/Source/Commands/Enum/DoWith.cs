using JetBrains.Annotations;

namespace SharpPipe {
    public static partial class Pipe {
        /// <summary>
        /// Adds a WITH command constraint.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static DoWith WITH([CanBeNull] object obj) => new DoWith(obj);
    }

    public struct DoWith {
        internal readonly object Object;

        public DoWith( [CanBeNull] object o ) => Object = o;
    }
}