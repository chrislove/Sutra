using JetBrains.Annotations;

namespace SharpPipe {
    public static partial class Commands {
        [PublicAPI]
        public static partial class func {
            [PublicAPI]
            public abstract class str : takes<string> { }

            [PublicAPI]
            public abstract class flt : takes<float> { }

            [PublicAPI]
            public abstract class integer : takes<int> { }

            [PublicAPI]
            public abstract class dbl : takes<double> { }
        }
    }
}