

using System.ComponentModel;

namespace SharpPipe {
    public static partial class Commands {
        public static DoNot NOT => new DoNot();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoNot { }
}