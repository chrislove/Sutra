using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace SharpPipe {
    /// <summary>
    /// Global pipe settings
    /// </summary>
    [PublicAPI]
    public static partial class Pipe {
        [EditorBrowsable(EditorBrowsableState.Never)] public static bool AllowNullInput  { get; set; } = false;

        [EditorBrowsable(EditorBrowsableState.Never)] public static bool AllowNullOutput { get; set; } = false;

        [ThreadStatic] [CanBeNull] private static Exception _nextException;

        [CanBeNull] public static Exception NextException {
            get {
                try {
                    return _nextException;
                } finally {
                    _nextException = null;
                }
            }
            set => _nextException = value;
        }
    }
}