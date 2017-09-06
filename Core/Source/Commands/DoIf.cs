using System;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Condition command constraint.
        /// </summary>
        public static DoIf IF => new DoIf();
    }

    public struct DoIf {}
    
    /*
    public partial struct EnumPipe<TOut> {
        public static DoIf<TOut> operator |( EnumPipe<TOut> pipe, DoIf doIf ) => new DoIf<TOut>(pipe);
    }


    public struct DoIf<T> {
        private readonly EnumPipe<T> _pipe;

        internal DoIf( EnumPipe<T> pipe ) => _pipe = pipe;

        public static DoIfConcrete<T> operator *( DoIf<T> doIf, [NotNull] Func<T, bool> predicate ) => new DoIfConcrete<T>(doIf, predicate);
    }

    public struct DoIfConcrete<T> {
        internal DoIfConcrete( DoIf<object> doIf, Func<object, bool> predicate ) {
        }
    }*/
}