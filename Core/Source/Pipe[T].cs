using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace SharpPipe {
    /// <summary>
    /// A pipe containing a single object.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct Pipe<T> : IPipe<T> {
        [NotNull] private Func<T> Func { get; }

        internal Pipe( [CanBeNull] T obj ) : this(() => obj) {
            if (obj == null && !PIPE.AllowNullInput)
                throw new NullPipeException($"Null object is not a valid input to {this.T()}");
        }

        private Pipe( [NotNull] Func<T> func ) : this() => Func = func ?? throw new ArgumentNullException(nameof(func));

        [NotNull] internal T Get {
            get {
                var result = Func();

                if (result == null && !PIPE.AllowNullOutput && !AllowNullOutput)
                    throw new NullPipeException($"'{this.T()}.Get' returned a null IEnumerable");

                return result;
            }
        }

        private bool AllowNullOutput { get; set; }
        
        public static Pipe<T> operator |( Pipe<T> pipe, Func<T, T> rhs ) => new Pipe<T>( rhs(pipe.Get) );

        /// <summary>
        /// Replaces pipe contents with an object
        /// </summary>
        public static Pipe<T> operator |( Pipe<T> pipe, T rhs ) => new Pipe<T>(rhs);
    }
}