using System.ComponentModel;

namespace SharpPipe {
    public static partial class Commands
    {
        /// <summary>
        /// Invokes Fun{T} or Act{T}.
        /// </summary>
        public static DoInvoke inv => new DoInvoke();
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoInvoke { }
}