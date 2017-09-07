using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static class PIPE<T> {
        /// <summary>
        /// Creates an empty Pipe{T}
        /// </summary>
        public static Pipe<T> NEW => PIPE.IN(default(T));
    }

    /// <summary>
    /// Pipe{T} factory
    /// </summary>
    public static partial class PIPE {
        /// <summary>
        /// Creates and initializes Pipe{T} with an object.
        /// </summary>
        public static Pipe<TOut> IN<TOut>([CanBeNull] TOut obj) => IN(SharpFunc.WithValue(obj));
        
        public static Pipe<TOut> IN<TOut>(SharpFunc<TOut> func) => new Pipe<TOut>(func);
        public static Pipe<TOut> IN<TIn, TOut>(SharpFunc<TIn, TOut> func)
            => new Pipe<TOut>( SharpFunc.FromFunc(i => func.Func(i.To<TIn>($"{nameof(IN)} {func.T()}") ) ) );
        
        /// <summary>
        /// Creates an empty EnumPipe{string}
        /// </summary>
        public static Pipe<string> STR => PIPE<string>.NEW;
        
        /// <summary>
        /// Creates an empty EnumPipe{int}
        /// </summary>
        public static Pipe<int> INT => PIPE<int>.NEW;
        
        /// <summary>
        /// Creates an empty EnumPipe{float}
        /// </summary>
        public static Pipe<float> FLOAT => PIPE<float>.NEW;
        
        /// <summary>
        /// Creates an empty EnumPipe{double}
        /// </summary>
        public static Pipe<double> DBL => PIPE<double>.NEW;
    }
}