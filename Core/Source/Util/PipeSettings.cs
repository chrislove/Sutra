using System;
using JetBrains.Annotations;

namespace SharpPipe {
    /// <summary>
    /// Global pipe settings
    /// </summary>
    [PublicAPI]
    public static partial class Pipe {
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