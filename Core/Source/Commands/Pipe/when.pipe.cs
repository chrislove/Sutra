using System;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public partial struct Pipe<T> {
        [NotNull]
        public static DoWhenPipe<T> operator |( Pipe<T> pipe, DoWhen doWhen ) => new DoWhenPipe<T>(pipe);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoWhenPipe<T> : Command<T> {
        public DoWhenPipe( Pipe<T> pipe ) : base(pipe) { }
        
        [NotNull]
        public static DoWhenPipeWithPredicate<T> operator |( [CanBeNull] DoWhenPipe<T> doWhen, [NotNull] Func<T, bool> predicate )
            => new DoWhenPipeWithPredicate<T>(doWhen, predicate);

        [NotNull]
        public static DoWhenPipeWithPredicate<T> operator |( [CanBeNull] DoWhenPipe<T> doWhen, [NotNull] Func<bool> predicate )
            => new DoWhenPipeWithPredicate<T>(doWhen, predicate);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoWhenPipeWithPredicate<T> : DoWhenSeq<T> {
        internal readonly Func<T, bool> Predicate;

        internal DoWhenPipeWithPredicate( DoWhenPipe<T> doWhen, Func<T, bool> predicate ) : base(doWhen) => Predicate = predicate;
        internal DoWhenPipeWithPredicate( DoWhenPipe<T> doWhen, Func<bool> predicate )    : base(doWhen) => Predicate = _ => predicate();
        internal DoWhenPipeWithPredicate( DoWhenPipeWithPredicate<T> doWhen )             : base(doWhen) => Predicate = doWhen.Predicate;
    
        [NotNull]
        public static DoWhenPipeWithPredicateSelect<T> operator |( [CanBeNull] DoWhenPipeWithPredicate<T> doWhen, DoSelect doSelect )
            => new DoWhenPipeWithPredicateSelect<T>(doWhen);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoWhenPipeWithPredicateSelect<T> : DoWhenPipeWithPredicate<T> {
        internal DoWhenPipeWithPredicateSelect( DoWhenPipeWithPredicate<T> doWhen ) : base(doWhen) {}
        
        public static Pipe<T> operator |( [NotNull] DoWhenPipeWithPredicateSelect<T> doSelectPipe, [NotNull] Func<T, T> func ) {
            if (doSelectPipe == null) throw new ArgumentNullException(nameof(doSelectPipe));
            if (func == null) throw new ArgumentNullException(nameof(func));
            
            var pipe = (Pipe<T>) doSelectPipe.Pipe;
            
            if (doSelectPipe.Predicate(pipe.get))
                return func(pipe.get) | to<T>.pipe;

            return pipe;
        }
    }
}