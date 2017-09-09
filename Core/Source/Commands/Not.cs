// ReSharper disable InconsistentNaming

using System.ComponentModel;

namespace SharpPipe {
    public static partial class Commands {
        public static DoNot NOT => new DoNot();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoNot { }
}