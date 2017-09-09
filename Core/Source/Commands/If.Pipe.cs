using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public partial struct Pipe<T> {
        [NotNull]
        public static DoIfPipe<T> operator |( Pipe<T> pipe, DoIf doIf ) => new DoIfPipe<T>(pipe);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoIfPipe<T> : Command<T> {
        public DoIfPipe( Pipe<T> pipe ) : base(pipe) { }
        
        [NotNull]
        public static DoIfPipeWithPredicate<T> operator |( [CanBeNull] DoIfPipe<T> doIf, [NotNull] Func<T, bool> predicate )
            => new DoIfPipeWithPredicate<T>(doIf, predicate);

        [NotNull]
        public static DoIfPipeWithPredicate<T> operator |( [CanBeNull] DoIfPipe<T> doIf, [NotNull] Func<bool> predicate )
            => new DoIfPipeWithPredicate<T>(doIf, predicate);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoIfPipeWithPredicate<T> : DoIfEnum<T> {
        internal readonly Func<T, bool> Predicate;

        internal DoIfPipeWithPredicate( DoIfPipe<T> doIf, Func<T, bool> predicate ) : base(doIf) => Predicate = predicate;
        internal DoIfPipeWithPredicate( DoIfPipe<T> doIf, Func<bool> predicate )    : base(doIf) => Predicate = _ => predicate();
        internal DoIfPipeWithPredicate( DoIfPipeWithPredicate<T> doIf )             : base(doIf) => Predicate = doIf.Predicate;
    
        [NotNull]
        public static DoIfPipeWithPredicateSelect<T> operator |( [CanBeNull] DoIfPipeWithPredicate<T> doIf, DoSelect doSelect )
            => new DoIfPipeWithPredicateSelect<T>(doIf);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoIfPipeWithPredicateSelect<T> : DoIfPipeWithPredicate<T> {
        internal DoIfPipeWithPredicateSelect( DoIfPipeWithPredicate<T> doIf ) : base(doIf) {}
        
        public static Pipe<T> operator |( [NotNull] DoIfPipeWithPredicateSelect<T> doSelectPipe, [NotNull] Func<T, T> func ) {
            if (doSelectPipe == null) throw new ArgumentNullException(nameof(doSelectPipe));
            if (func == null) throw new ArgumentNullException(nameof(func));
            
            var pipe = (Pipe<T>) doSelectPipe.Pipe;
            
            if (doSelectPipe.Predicate(pipe.Get))
                return func(pipe.Get) | TO<T>.PIPE;

            return pipe;
        }
    }
}