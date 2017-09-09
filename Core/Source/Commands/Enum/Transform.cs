// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class Commands {
        public static DoTransform TRANSFORM => new DoTransform();
    }

    public struct DoTransform { }
    
    public partial struct EnumPipe<TOut> {
        public static DoTransform<TOut> operator |( EnumPipe<TOut> pipe, DoTransform @do ) => new DoTransform<TOut>(pipe);
    }

    public struct DoTransform<T> {
        private readonly EnumPipe<T> _pipe;

        internal DoTransform( EnumPipe<T> pipe ) => _pipe = pipe;

        public static EnumPipe<T> operator |( DoTransform<T> @do, [NotNull] Func<IEnumerable<T>, IEnumerable<T>> func )
            => func(@do._pipe.Get) | TO<T>.PIPE;
    }
}