using System;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

// ReSharper disable InconsistentNaming

namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Filters contents of EnumerablePipe
        /// </summary>
        public static DoWhere WHERE => new DoWhere();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoWhere { }

    public partial struct EnumerablePipe<T> {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static DoWhere<T> operator |( EnumerablePipe<T> pipe, DoWhere @where ) => new DoWhere<T>(pipe);
    }
    

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoWhere<T> {
        internal readonly EnumerablePipe<T> Pipe;

        internal DoWhere( EnumerablePipe<T> pipe ) => Pipe = pipe;

        public static EnumerablePipe<T> operator |( DoWhere<T> @where, Func<T, bool> predicate ) => where.Pipe.Get.Where(predicate) | TO<T>.PIPE;
        
        public static DoWhereIf<T> operator |( DoWhere<T> @where, DoIf doIf ) => new DoWhereIf<T>(@where);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoWhereIf<T> {
        private readonly DoWhere<T> _doWhere;

        internal DoWhereIf( DoWhere<T> doWhere ) => _doWhere = doWhere;

        public static EnumerablePipe<T> operator |( DoWhereIf<T> doWhereIf, [NotNull] Func<T, bool> predicate ) {
            var pipe = doWhereIf._doWhere.Pipe;
            var filtered = pipe.Get.Where(predicate);

            return START<T>.PIPE | filtered;
        }
    }
}