// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        public static class START<T> {
            public static DoToPipe<T> PIPE => new DoToPipe<T>();
        }

        public static class START {
            /// <summary>
            /// Starts a {string} pipe.
            /// </summary>
            public static class STRING {
                public static DoToPipe<string> PIPE => START<string>.PIPE;
            }

            /// <summary>
            /// Starts a {int} pipe.
            /// </summary>
            public static class INT {
                public static DoToPipe<int> PIPE => START<int>.PIPE;
            }

            /// <summary>
            /// Starts a {float} pipe.
            /// </summary>
            public static class FLOAT {
                public static DoToPipe<float> PIPE => START<float>.PIPE;
            }

            /// <summary>
            /// Starts a {double} pipe.
            /// </summary>
            public static class DOUBLE {
                public static DoToPipe<double> PIPE => START<double>.PIPE;
            }

            /// <summary>
            /// Starts a {DateTime} pipe.
            /// </summary>
            public static class DATETIME {
                public static DoToPipe<DateTime> PIPE => START<DateTime>.PIPE;
            }
        }
    }
}