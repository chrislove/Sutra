using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using SharpPipe.Transformations;
using static SharpPipe.Commands;

namespace SharpPipe {
    /// <summary>
    /// A set of pipe command factories.
    /// </summary>
    public static partial class Commands {
        /// <summary>
        /// Starts a new pipe of built-in type.
        /// </summary>
        [PublicAPI]
        public static class start {
            /// <summary>
            /// Starts a pipe.
            /// </summary>
            public static DoStartPipe pipe => new DoStartPipe();
            
            /// <summary>
            /// Starts a sequence.
            /// </summary>
            public static DoStartSeq seq => new DoStartSeq();
        }
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoStartPipe
    {
        [PublicAPI]public DoStartPipe<T> of<T>() => new DoStartPipe<T>();
        
        [PublicAPI] public Pipe<T> with<T>( T obj ) => start.pipe.of<T>() | obj;
        [PublicAPI] public Pipe<T> with<T>( Option<T> obj ) => start.pipe.of<T>() | obj;
        
        public static Pipe<string> operator |( DoStartPipe _, [NotNull] string obj ) => start.pipe.with(obj);
        public static Pipe<string> operator |( DoStartPipe _, Option<string> obj )   => start.pipe.with(obj);
        
        public static Pipe<str> operator |( DoStartPipe _, str obj ) => start.pipe.with(obj);
        
        public static Pipe<int> operator |( DoStartPipe _, int obj ) => start.pipe.with(obj);
        public static Pipe<int> operator |( DoStartPipe _, Option<int> obj )   => start.pipe.with(obj);
        
        public static Pipe<float> operator |( DoStartPipe _, float obj ) => start.pipe.with(obj);
        public static Pipe<float> operator |( DoStartPipe _, Option<float> obj )   => start.pipe.with(obj);
        
        public static Pipe<double> operator |( DoStartPipe _, double obj ) => start.pipe.with(obj);
        public static Pipe<double> operator |( DoStartPipe _, Option<double> obj )   => start.pipe.with(obj);
        
        public static Pipe<DateTime> operator |( DoStartPipe _, DateTime obj ) => start.pipe.with(obj);
        public static Pipe<DateTime> operator |( DoStartPipe _, Option<DateTime> obj )   => start.pipe.with(obj);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoStartSeq
    {
        [PublicAPI]public DoStartSeq<T> of<T>() => new DoStartSeq<T>();

        [PublicAPI] public Seq<T> with<T>( IEnumerable<T> obj ) => start.seq.of<T>() | obj;
        [PublicAPI] public Seq<T> with<T>( IEnumerable<Option<T>> obj ) => start.seq.of<T>() | obj;
        
        public static Seq<string> operator |( DoStartSeq _, [NotNull] IEnumerable<string> obj ) => start.seq.with(obj);
        public static Seq<string> operator |( DoStartSeq _, IEnumerable<Option<string>> obj )   => start.seq.with(obj);
        
        public static Seq<str> operator |( DoStartSeq _, [NotNull] IEnumerable<str> obj ) => start.seq.with(obj);
        
        public static Seq<int> operator |( DoStartSeq _, IEnumerable<int> obj ) => start.seq.with(obj);
        public static Seq<int> operator |( DoStartSeq _, IEnumerable<Option<int>> obj )   => start.seq.with(obj);
        
        public static Seq<float> operator |( DoStartSeq _, IEnumerable<float> obj ) => start.seq.with(obj);
        public static Seq<float> operator |( DoStartSeq _, IEnumerable<Option<float>> obj )   => start.seq.with(obj);
        
        public static Seq<double> operator |( DoStartSeq _, IEnumerable<double> obj ) => start.seq.with(obj);
        public static Seq<double> operator |( DoStartSeq _, IEnumerable<Option<double>> obj )   => start.seq.with(obj);
        
        public static Seq<DateTime> operator |( DoStartSeq _, IEnumerable<DateTime> obj ) => start.seq.with(obj);
        public static Seq<DateTime> operator |( DoStartSeq _, IEnumerable<Option<DateTime>> obj )   => start.seq.with(obj);
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