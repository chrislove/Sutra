using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using Sutra.Transformations;
using static Sutra.Commands;

namespace Sutra
{
    /// <summary>
    /// A sequence monad containing a set of objects.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial struct Seq<T> : IPipe<T>, IEnumerable<Option<T>>
    {
        internal readonly SeqOption<T> Option;

        internal Seq( SeqOption<T> option ) => Option = option;
        internal Seq( [CanBeNull] IEnumerable<Option<T>> enm ) => Option = enm.Return();
        internal Seq( [CanBeNull] IEnumerable<T> enm ) => Option = enm.Return().Return();

        [Pure]
        internal Seq<TOut> Map<TOut>( [NotNull] Func<IEnumerable<Option<T>>, IEnumerable<Option<TOut>>> func ) => start.seq.of<TOut>() | Option.Map(func);

        [Pure]
        internal Seq<TOut> Map<TOut>( [NotNull] Func<IEnumerable<IOption>, IEnumerable<Option<TOut>>> func ) => start.seq.of<TOut>() | Option.Map(func);

        [Pure]
        internal Seq<TOut> Map<TOut>( [NotNull] Func<IEnumerable<T>, IEnumerable<TOut>> func ) => start.seq.of<TOut>() | Option.Map(func);

        /// <summary>
        /// Appends a single object to sequence.
        /// </summary>
        [Pure]
        public static Seq<T> operator |( Seq<T> seq, [CanBeNull] T obj )
            {

                return seq | obj.Yield();
            }
        
        /// <summary>
        /// Appends a single object to sequence.
        /// </summary>
        [Pure]
        public static Seq<T> operator |( Seq<T> seq, Option<T> obj )
            {

                return seq | obj.Yield();
            }

        /// <summary>
        /// Appends an enumerable to sequence.
        /// </summary>
        [Pure]
        public static Seq<T> operator |( Seq<T> seq, [CanBeNull] IEnumerable<T> enm ) => seq | enm.Return();

        /// <summary>
        /// Appends an enumerable to sequence.
        /// </summary>
        [Pure]
        public static Seq<T> operator |( Seq<T> seq, [CanBeNull] IEnumerable<Option<T>> enm )
            {
                return seq.Option.Map(seqEnm => seqEnm?.Concat(enm));
            }

        /// <summary>
        /// Transforms sequence.
        /// </summary>
        /// <returns></returns>
        [Pure]
        public static Seq<T> operator |( Seq<T> seq, [NotNull] Func<IEnumerable<T>, IEnumerable<T>> func ) => seq.Map(func);

        /// <summary>
        /// Transforms sequence.
        /// </summary>
        /// <returns></returns>
        [Pure]
        public static Seq<T> operator |( Seq<T> seq, [NotNull] Func<IEnumerable<IOption>, IEnumerable<Option<T>>> func ) => seq.Map(func);

        /// <summary>
        /// Transforms sequence into a pipe.
        /// </summary>
        [Pure]
        public static Pipe<T> operator |( Seq<T> seq, [NotNull] Func<IEnumerable<Option<T>>, Option<T>> func ) => func(seq);

        public static implicit operator Seq<T>( SeqOption<T> option ) => start.seq.of<T>() | option;
        
        public IEnumerator<Option<T>> GetEnumerator()
            {
                foreach (IEnumerable<Option<T>> enm in Option.Enm)
                    return enm.GetEnumerator();

                return Enumerable.Empty<Option<T>>().GetEnumerator();
            }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}