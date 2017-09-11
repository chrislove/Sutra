

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
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoAct {}

    public partial struct Pipe<T> {
        public static DoAct<T> operator |( Pipe<T> pipe, DoAct doAct ) => new DoAct<T>(pipe);
    }

    public struct DoAct<T> {
        private readonly Pipe<T> _pipe;

        public DoAct( Pipe<T> pipe ) => _pipe = pipe;

        public static Unit operator |( DoAct<T> doAct, [NotNull] Action<T> act ) => ( () => act(doAct._pipe.Get) ) | unit;

    }
}