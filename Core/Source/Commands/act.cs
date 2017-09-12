

using System;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Performs an action on the contents of the pipe.
        /// </summary>
        public static DoAct act => new DoAct();
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAct {}

    public partial struct Pipe<T> {
        public static DoAct<T> operator |( Pipe<T> pipe, DoAct _ ) => new DoAct<T>(pipe);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    public struct DoAct<T> {
        private readonly Pipe<T> _pipe;

        internal DoAct( Pipe<T> pipe ) => _pipe = pipe;

        public static Unit operator |( DoAct<T> doAct, [NotNull] Action<Option<T>> act ) {
            return (() => act(doAct._pipe.Get)) | unit;
        }


        /// <summary>
        /// Executes the action on the right.
        /// </summary>
        public static Unit operator |( DoAct<T> doAct, [NotNull] Action<T> act ) {
            foreach (var value in doAct._pipe.Get)
                return (() => act(value)) | unit;

            return unit;
        }
    }
}