using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using SharpPipe.Transformations;

namespace SharpPipe
{
    public static partial class Commands
    {
        /// <summary>
        /// Filters contents of Sequence
        /// </summary>
        public static DoWhere where => new DoWhere();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoWhere { }

    public partial struct Seq<T>
    {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static DoWhereSeq<T> operator |( Seq<T> seq, DoWhere _ ) => new DoWhereSeq<T>(seq);
    }
    
    public partial struct Pipe<T>
    {
        /// <summary>
        /// Converts pipe contents into TOut[]
        /// </summary>
        public static DoWherePipe<T> operator |( Pipe<T> pipe, DoWhere _ ) => new DoWherePipe<T>(pipe);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoWhereSeq<T>
    {
        private readonly Seq<T> _seq;

        internal DoWhereSeq( Seq<T> pipe ) => _seq = pipe;

        public static Seq<T> operator |( DoWhereSeq<T> doWhere, [NotNull] Func<IOption, bool> predicate )
            {
                Func<IEnumerable<IOption>, IEnumerable<Option<T>>> func = enm => enm.Where(predicate).Cast<Option<T>>();
                return doWhere._seq.Map(func);
            }

        public static Seq<T> operator |( DoWhereSeq<T> doWhere, [NotNull] Func<T, bool> predicate )
            {
                return doWhere | predicate.Map().Cast().InTo<IOption>();
            }
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoWherePipe<T>
    {
        private readonly Pipe<T> _pipe;

        internal DoWherePipe( Pipe<T> pipe ) => _pipe = pipe;

        public static Pipe<T> operator |( DoWherePipe<T> doWhere, [NotNull] Func<IOption, bool> predicate )
            {
                Func<Option<T>, Option<T>> func = option => predicate(option) ? option : default;
                
                return doWhere._pipe.Map(func);
            }

        public static Pipe<T> operator |( DoWherePipe<T> doWhere, [NotNull] Func<T, bool> predicate )
            {
                return doWhere | predicate.Map().Cast().InTo<IOption>();
            }
    }
}