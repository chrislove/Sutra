using System;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public partial struct Pipe<T> {
        /// <summary>
        /// Sets up conditional pipe transformation.
        /// </summary>
        [NotNull]
        public static DoWhenPipe<T> operator |( Pipe<T> pipe, DoWhen _ ) => new DoWhenPipe<T>(pipe);
    }

    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoWhenPipe<T> : Command<T> {
        internal DoWhenPipe( Pipe<T> pipe ) : base(pipe) { }
        
        /// <summary>
        /// Sets up pipe transformation condition.
        /// </summary>
        [NotNull]
        public static DoWhenPipeWithPredicate<T> operator |( [CanBeNull] DoWhenPipe<T> doWhen, [NotNull] Func<T, bool> predicate )
            => new DoWhenPipeWithPredicate<T>(doWhen, predicate);

        /// <summary>
        /// Sets up pipe transformation condition.
        /// </summary>
        [NotNull]
        public static DoWhenPipeWithPredicate<T> operator |( [CanBeNull] DoWhenPipe<T> doWhen, [NotNull] Func<bool> predicate )
            => new DoWhenPipeWithPredicate<T>(doWhen, predicate);
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoWhenPipeWithPredicate<T> : DoWhenSeq<T> {
        [NotNull]
        internal readonly Func<T, bool> Predicate;

        internal DoWhenPipeWithPredicate( [NotNull] DoWhenPipe<T> doWhen, [NotNull] Func<T, bool> predicate ) : base(doWhen) {
            Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        internal DoWhenPipeWithPredicate( [NotNull] DoWhenPipe<T> doWhen, [NotNull] Func<bool> predicate )    : base(doWhen) {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            Predicate = _ => predicate();
        }

        internal DoWhenPipeWithPredicate( [NotNull] DoWhenPipeWithPredicate<T> doWhen ) : base(doWhen) => Predicate = doWhen.Predicate;
    
        /// <summary>
        /// Sets up conditional 'map' pipe transformation.
        /// </summary>
        [NotNull]
        public static DoWhenPipeWithPredicateSelect<T> operator |( [CanBeNull] DoWhenPipeWithPredicate<T> doWhen, DoMap _ )
                                => new DoWhenPipeWithPredicateSelect<T>(doWhen);
    }
    
    /// <summary>
    /// Command marker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class DoWhenPipeWithPredicateSelect<T> : DoWhenPipeWithPredicate<T> {
        internal DoWhenPipeWithPredicateSelect( DoWhenPipeWithPredicate<T> doWhen ) : base(doWhen) {}
        
        /// <summary>
        /// Performs pipe transformation.
        /// </summary>
        public static Pipe<T> operator |( [NotNull] DoWhenPipeWithPredicateSelect<T> doSelectPipe, [NotNull] Func<T, T> func ) {
            if (doSelectPipe == null) throw new ArgumentNullException(nameof(doSelectPipe));
            if (func == null) throw new ArgumentNullException(nameof(func));
            
            var pipe = (Pipe<T>) doSelectPipe.Pipe;
            
            foreach (var value in pipe.Value) {
                if (doSelectPipe.Predicate(value))
                    return start<T>.pipe | func(value);
            }
            
            return pipe;
        }
    }
}