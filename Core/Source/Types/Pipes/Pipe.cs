using System;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    /// <summary>
    /// A pipe containing a single object.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct Pipe<T> : IPipe<T> {
        [NotNull] private readonly T _contents;

        internal Pipe( [NotNull] T value ) : this() {
            if (value == null && !Pipe.AllowNullInput) throw new NullPipeException($"Null object is not a valid input to {nameof(Pipe<T>)}");
            _contents = value;
        }
        
        [NotNull] internal T Get {
            get {
                if (_contents == null && !Pipe.AllowNullOutput && !AllowNullOutput)
                    throw new NullPipeException($"'{this.T()}.Get' returned a null IEnumerable");

                return _contents;
            }
        }

        private bool AllowNullOutput { get; set; }
        
        /// <summary>
        /// Transforms pipe contents using a function on the right.
        /// </summary>
        public static Pipe<T> operator |( Pipe<T> pipe, Func<T, T> func ) => start<T>.pipe | func(pipe.Get);

        /// <summary>
        /// Replaces pipe contents with object on the right.
        /// </summary>
        public static Pipe<T> operator |( Pipe<T> pipe, T obj ) => start<T>.pipe | obj;
    }
}