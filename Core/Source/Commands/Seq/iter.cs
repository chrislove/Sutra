using System;
using System.ComponentModel;
using JetBrains.Annotations;
using Sutra.Transformations;
using static Sutra.Commands;

namespace Sutra
{
    public static partial class Commands
    {
        /// <summary>
        /// Performs an action on each element of a sequence.
        /// </summary>
        public static DoIterate iter => new DoIterate();
        
        /// <summary>
        /// Performs an action on each element of a sequence.
        /// </summary>
        public static DoIterateFunc iterf => new DoIterateFunc();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoIterate { }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoIterateFunc { }

    public partial struct Seq<T>
    {
        /// <summary>
        /// Sets up the iter command.
        /// </summary>
        public static DoIterate<T> operator |( Seq<T> seq, DoIterate _ ) => new DoIterate<T>(seq);
        
        /// <summary>
        /// Sets up the iter command.
        /// </summary>
        public static DoIterateFunc<T> operator |( Seq<T> seq, DoIterateFunc _ ) => new DoIterateFunc<T>(seq);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    public struct DoIterate<T>
    {
        private readonly Seq<T> _seq;

        internal DoIterate( Seq<T> seq ) => _seq = seq;
        
        /// <summary>
        /// Performs the action on the right for each element of the sequence.
        /// </summary>
        public static Seq<T> operator |( DoIterate<T> doIterate, [NotNull] Action<Option<T>> action ) => doIterate._seq | iterf | action.ReturnsUnit();

        /// <summary>
        /// Performs the action on the right for each element of the sequence.
        /// </summary>
        public static Seq<T> operator |( DoIterate<T> doIterate, [NotNull] Action<Some<T>> action ) => doIterate._seq | iterf | action.ReturnsUnit();

        /// <summary>
        /// Performs the action on the right for each non-empty element of the sequence.
        /// </summary>
        public static Seq<T> operator |( DoIterate<T> doIterate, [NotNull] Action<T> action ) => doIterate._seq | iterf | action.Map();

    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    public struct DoIterateFunc<T>
    {
        private readonly Seq<T> _seq;

        internal DoIterateFunc( Seq<T> seq ) => _seq = seq;
        
        /// <summary>
        /// Performs the action on the right for each element of the sequence.
        /// </summary>
        public static Seq<T> operator |( DoIterateFunc<T> doIterate, [NotNull] Func<Option<T>, Unit> unitFunc )
                    => unitFunc.Map()(doIterate._seq.Option).Return(doIterate._seq);
        /// <summary>
        /// Performs the action on the right for each element of the sequence.
        /// </summary>
        public static Seq<T> operator |( DoIterateFunc<T> doIterate, [NotNull] Func<Some<T>, Unit> unitFunc )
            => doIterate | unitFunc.ToOptionFunc();


        /// <summary>
        /// Performs the action on the right for each element of the sequence.
        /// </summary>
        public static Seq<T> operator |( DoIterateFunc<T> doIterate, [NotNull] Func<T, Unit> unitFunc ) => doIterate | unitFunc.Map().ValueOr(unit);
    }
}