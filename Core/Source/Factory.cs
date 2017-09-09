// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static class NEW<T> {
        public static DoStartPipe<T> PIPE => new DoStartPipe<T>();
    }

    public static class NEW {
        /// <summary>
        /// Starts a {string} pipe.
        /// </summary>
        public static class STRING {
            public static DoStartPipe<string> PIPE => NEW<string>.PIPE;

        }

        /// <summary>
        /// Starts a {int} pipe.
        /// </summary>
        public static class INT {
            public static DoStartPipe<int> PIPE => NEW<int>.PIPE;
        }
        
        /// <summary>
        /// Starts a {float} pipe.
        /// </summary>
        public static class FLOAT {
            public static DoStartPipe<float> PIPE => NEW<float>.PIPE;
        }
        
        /// <summary>
        /// Starts a {double} pipe.
        /// </summary>
        public static class DOUBLE {
            public static DoStartPipe<double> PIPE => NEW<double>.PIPE;
        }
        
        /// <summary>
        /// Starts a {double} pipe.
        /// </summary>
        public static class DATETIME {
            public static DoStartPipe<DateTime> PIPE => NEW<DateTime>.PIPE;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct DoStartPipe<T> {
        public static Pipe<T> operator |( DoStartPipe<T> doStartPipe, T obj ) => new Pipe<T>(obj);
        
        public static EnumPipe<T> operator |( DoStartPipe<T> doStartPipe, IEnumerable<T> enumerable ) => new EnumPipe<T>( enumerable );
    
    }
}