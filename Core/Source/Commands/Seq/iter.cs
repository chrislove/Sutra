using System;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using SharpPipe.Transformations;
using static SharpPipe.Commands;

namespace SharpPipe
{
    public static partial class Commands
    {
        /// <summary>
        /// Performs an action on the contents of the sequence.
        /// </summary>
        public static DoIterate iter => new DoIterate();
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct DoIterate { }

    public partial struct Seq<T>
    {
        /// <summary>
        /// Sets up the iter command.
        /// </summary>
        public static DoIterate<T> operator |( Seq<T> seq, DoIterate _ ) => new DoIterate<T>(seq);
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
        public static Unit operator |( DoIterate<T> doIterate, [NotNull] Func<Option<T>, Unit> unitFunc )
            {
                return unitFunc.Map()(doIterate._seq.Option);
            }

        /// <summary>
        /// Performs the action on the right for each element of the sequence.
        /// </summary>
        public static Unit operator |( DoIterate<T> doIterate, [NotNull] Action<Option<T>> action )
            {
                return doIterate | action.ReturnsUnit();
            }

        /// <summary>
        /// Performs the action on the right for each non-empty element of the sequence.
        /// </summary>
        public static Unit operator |( DoIterate<T> doIterate, [NotNull] Action<T> action ) => doIterate | action.Map();

        /// <summary>
        /// Performs the action on the right for each non-empty element of the sequence.
        /// </summary>
        public static Unit operator |( DoIterate<T> doIterate, [NotNull] Action<object> action ) => doIterate | (( T i ) => action(i));
    }
}