using System;
using System.ComponentModel;
using JetBrains.Annotations;
using SharpPipe.Transformations;
using static SharpPipe.Commands;

namespace SharpPipe
{
    public static partial class Commands
    {
        /// <summary>
        /// Use for debugging sequences or pipes.
        /// </summary>
        public static DoDebug dbg => new DoDebug();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoDebug { }

    public partial struct Seq<T>
    {
        /// <summary>
        /// Sets up the dbg command.
        /// </summary>
        public static DoDebugSeq<T> operator |( Seq<T> seq, DoDebug _ ) => new DoDebugSeq<T>(seq);
    }

    public partial struct Pipe<T>
    {
        /// <summary>
        /// Sets up the dbg command.
        /// </summary>
        public static DoDebugPipe<T> operator |( Pipe<T> pipe, DoDebug _ ) => new DoDebugPipe<T>(pipe);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    public struct DoDebugSeq<T>
    {
        private readonly Seq<T> _seq;

        internal DoDebugSeq( Seq<T> seq ) => _seq = seq;

        public static Seq<T> operator |( DoDebugSeq<T> doDebug, [NotNull] Func<Option<T>, Unit> unitFunc )
            {
                Unit unit = doDebug._seq | iter | unitFunc;

                return doDebug._seq;
            }


        /// <summary>
        /// Performs the action on the right for each element of the sequence, and returns the sequence.
        /// </summary>
        public static Seq<T> operator |( DoDebugSeq<T> doDebug, [NotNull] Action<Option<T>> action )
            {
                Unit unit = doDebug._seq | iter | action;

                return doDebug._seq;
            }

        /// <summary>
        /// Performs the action on the right for each element of the sequence, and returns the sequence.
        /// </summary>
        public static Seq<T> operator |( DoDebugSeq<T> doDebug, Action<T> action )
            {
                Unit unit = doDebug._seq | iter | action;

                return doDebug._seq;
            }
        
        /// <summary>
        /// Executes the action on the right.
        /// </summary>
        public static Seq<T> operator |( DoDebugSeq<T> doDebug, Action<object> action ) => doDebug | ( ( T i ) => action(i) );
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    public struct DoDebugPipe<T>
    {
        private readonly Pipe<T> _pipe;

        internal DoDebugPipe( Pipe<T> pipe ) => _pipe = pipe;

        /// <summary>
        /// Executes the action on the right.
        /// </summary>
        public static Pipe<T> operator |( DoDebugPipe<T> doDebug, [NotNull] Action<Option<T>> action )
            {
                Unit unit = doDebug._pipe | act | action;
                
                return doDebug._pipe;
            }

        /// <summary>
        /// Executes the action on the right.
        /// </summary>
        public static Pipe<T> operator |( DoDebugPipe<T> doDebug, [NotNull] Action<T> action )
            {
                Unit unit = doDebug._pipe | act | action;
                
                return doDebug._pipe;
            }
        
        /// <summary>
        /// Executes the action on the right.
        /// </summary>
        public static Pipe<T> operator |( DoDebugPipe<T> doDebug, [NotNull] Action<object> action ) => doDebug | ( ( T i ) => action(i) );
    }
}