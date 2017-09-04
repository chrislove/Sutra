using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    /// <summary>
    /// Pipe{T} factory
    /// </summary>
    public static class PIPE {
        /// <summary>
        /// Creates and initializes Pipe{T} with an object.
        /// </summary>
        public static Pipe<T> IN<T>([CanBeNull] T obj) => IN(SharpFunc.WithValue(obj));
        
        public static Pipe<T> IN<T>([NotNull] IOutFunc<T> func) => new Pipe<T>(func);

        /// <summary>
        /// Creates an empty Pipe{T}
        /// </summary>
        public static Pipe<T> NEW<T>() => IN(default(T));


        /// <summary>
        /// Creates an empty Pipe{string}
        /// </summary>
        public static Pipe<string> STR => NEW<string>();
        
        /// <summary>
        /// Creates an empty Pipe{int}
        /// </summary>
        public static Pipe<int> INT => NEW<int>();
        
        /// <summary>
        /// Creates an empty Pipe{float}
        /// </summary>
        public static Pipe<float> FLOAT => NEW<float>();
        
        /// <summary>
        /// Creates an empty Pipe{double}
        /// </summary>
        public static Pipe<double> DBL => NEW<double>();
    }
}