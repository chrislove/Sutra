// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Condition command constraint.
        /// </summary>
        public static DoIfAny IFANY => new DoIfAny();
        
        /// <summary>
        /// Condition command constraint.
        /// </summary>
        public static DoIf IF => new DoIf();
    }

    public struct DoIfAny {}
    public struct DoIf {}
}