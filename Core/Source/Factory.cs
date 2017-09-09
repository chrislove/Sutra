// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.ComponentModel;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static class NEW<T> {
        public static DoStartPipe<T> PIPE => new DoStartPipe<T>();
    }

    public static class NEW {
        /// <summary>
        /// Starts a {string} pipe.
        /// </summary>
        public static DoStart<string> STRING => new DoStart<string>();
        
        /// <summary>
        /// Starts an {int} pipe
        /// </summary>
        public static DoStart<int> INT => new DoStart<int>();
        
        /// <summary>
        /// Starts a {float} pipe
        /// </summary>
        public static DoStart<float> FLOAT => new DoStart<float>();
        
        /// <summary>
        /// Starts a {double} pipe
        /// </summary>
        public static DoStart<double> DOUBLE => new DoStart<double>();
        
        /// <summary>
        /// Starts a {DateTime} pipe
        /// </summary>
        public static DoStart<DateTime> DATETIME => new DoStart<DateTime>();
    }


    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoStart<T> {
        public DoStartPipe<T> PIPE => new DoStartPipe<T>();
    }
    

    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct DoStartPipe<T> {
        public static Pipe<T> operator |( DoStartPipe<T> doStartPipe, T obj ) => new Pipe<T>(obj);
        
        public static EnumPipe<T> operator |( DoStartPipe<T> doStartPipe, IEnumerable<T> enumerable ) => NEW<T>.PIPE | ADD | enumerable;
    
    }
}