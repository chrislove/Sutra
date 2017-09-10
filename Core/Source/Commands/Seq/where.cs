using System;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;



namespace SharpPipe {
    public static partial class Commands {
        /// <summary>
        /// Filters contents of Sequence
        /// </summary>
        public static DoWhere where => new DoWhere();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoWhere { }

    public partial struct Seq<T> {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static DoWhere<T> operator |( Seq<T> pipe, DoWhere where ) => new DoWhere<T>(pipe);
    }
    

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoWhere<T> {
        internal readonly Seq<T> Pipe;

        internal DoWhere( Seq<T> pipe ) => Pipe = pipe;

        public static Seq<T> operator |( DoWhere<T> where, Func<T, bool> predicate ) => where.Pipe.get.Where(predicate) | to<T>.pipe;
        
        public static DoWhereIf<T> operator |( DoWhere<T> where, DoWhen doWhen ) => new DoWhereIf<T>(where);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoWhereIf<T> {
        private readonly DoWhere<T> _doWhere;

        internal DoWhereIf( DoWhere<T> doWhere ) => _doWhere = doWhere;

        public static Seq<T> operator |( DoWhereIf<T> doWhereIf, [NotNull] Func<T, bool> predicate ) {
            var pipe = doWhereIf._doWhere.Pipe;
            var filtered = pipe.get.Where(predicate);

            return start<T>.pipe | filtered;
        }
    }
}