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
        /// Performs an action on the contents of the pipe.
        /// </summary>
        public static DoTee tee => new DoTee();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoTee { }

    public partial struct Pipe<T>
    {
        /// <summary>
        /// Sets up pipe action.
        /// </summary>
        public static DoTee<T> operator |( Pipe<T> pipe, DoTee _ ) => new DoTee<T>(pipe);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    public struct DoTee<T>
    {
        private readonly Pipe<T> _pipe;

        internal DoTee( Pipe<T> pipe ) => _pipe = pipe;

        /// <summary>
        /// Executes the action on the right.
        /// </summary>
        public static Unit operator |( DoTee<T> doTee, [NotNull] Action<Option<T>> act ) => act.ReturnsUnit()(doTee._pipe.Option);

        /// <summary>
        /// Executes the action on the right.
        /// </summary>
        public static Unit operator |( DoTee<T> doTee, [NotNull] Action<T> act ) => doTee | act.Map();
        
        /// <summary>
        /// Executes the action on the right.
        /// </summary>
        public static Unit operator |( DoTee<T> doTee, [NotNull] Func<Option<T>, Unit> func ) => func(doTee._pipe.Option);

        /// <summary>
        /// Executes the action on the right.
        /// </summary>
        public static Unit operator |( DoTee<T> doTee, [NotNull] Func<T, Unit> func ) => doTee | func.Map().ValueOr(unit);

    }
}