using System;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Condition command constraint.
        /// </summary>
        public static DoIf IF => new DoIf();
    }

    public struct DoIf {}
}