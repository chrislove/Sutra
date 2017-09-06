using System;

namespace SharpPipe {
    public static partial class Commands {
        public static Func<string, bool> IS = (i => i == null);
    }
}