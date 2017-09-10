namespace SharpPipe {
    public static partial class Commands {
        public static class start<T> {
            public static DoToPipe<T> pipe => new DoToPipe<T>();
        }

        public static class start {
            /// <summary>
            /// Starts a {string} pipe.
            /// </summary>
            public static class str {
                public static DoToPipe<string> pipe => start<string>.pipe;
            }

            /// <summary>
            /// Starts a {int} pipe.
            /// </summary>
            public static class integer {
                public static DoToPipe<int> pipe => start<int>.pipe;
            }

            /// <summary>
            /// Starts a {float} pipe.
            /// </summary>
            public static class flt {
                public static DoToPipe<float> pipe => start<float>.pipe;
            }

            /// <summary>
            /// Starts a {double} pipe.
            /// </summary>
            public static class dbl {
                public static DoToPipe<double> pipe => start<double>.pipe;
            }
        }
    }
}