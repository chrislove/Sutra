using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using SharpPipe.Transformations;

namespace SharpPipe {
    /// <summary>
    /// A set of pipe command factories.
    /// </summary>
    public static partial class Commands {
        /// <summary>
        /// Starts a new pipe of type {T}.
        /// </summary>
        [PublicAPI]
        public abstract class start<T> {
            internal start() { }
            
            /// <summary>
            /// Starts a pipe.
            /// </summary>
            public static DoStartPipe<T> pipe => new DoStartPipe<T>();
            
            /// <summary>
            /// Starts a sequence.
            /// </summary>
            public static DoStartSeq<T> seq => new DoStartSeq<T>();
        }

        /// <summary>
        /// Starts a new pipe of built-in type.
        /// </summary>
        [PublicAPI]
        public static class start {
            /// <summary>
            /// Starts a {string} pipe.
            /// </summary>
            [PublicAPI]
            public abstract class str : start<string> {
                private str() { }
            }

            /// <summary>
            /// Starts a {int} pipe.
            /// </summary>
            [PublicAPI]
            public abstract class integer : start<int> {
                private integer() { }
            }

            /// <summary>
            /// Starts a {float} pipe.
            /// </summary>
            [PublicAPI]
            public abstract class flt : start<float> {
                private flt() { }
            }

            /// <summary>
            /// Starts a {double} pipe.
            /// </summary>
            [PublicAPI]
            public abstract class dbl : start<double> {
                private dbl() { }
            }
        }
    }
        
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoStartPipe<T> {
        /// <summary>
        /// Initializes a pipe with object on the right
        /// </summary>
        public static Pipe<T> operator |( DoStartPipe<T> _, [NotNull] T obj )   => new Pipe<T>(obj.ToOption());
        
        /// <summary>
        /// Initializes a pipe with option on the right
        /// </summary>
        public static Pipe<T> operator |( DoStartPipe<T> _, Option<T> option )  => new Pipe<T>(option);
    } 
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct DoStartSeq<T> {
        /// <summary>
        /// Initializes a sequence with enumerable on the right
        /// </summary>
        public static Seq<T> operator |( DoStartSeq<T> _, [NotNull] IEnumerable<T> enm )  => new Seq<T>(enm);
        
        /// <summary>
        /// Initializes a sequence with enumerable on the right
        /// </summary>
        public static Seq<T> operator |( DoStartSeq<T> _, [NotNull] IEnumerable<Option<T>> enm )  => new Seq<T>(enm);
        
        /// <summary>
        /// Initializes a sequence with enumerable option on the right
        /// </summary>
        public static Seq<T> operator |( DoStartSeq<T> _, SeqOption<T> option ) => new Seq<T>(option);
    }
}