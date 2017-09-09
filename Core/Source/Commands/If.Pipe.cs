using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public partial struct Pipe<TOut> {
        [NotNull]
        public static DoIfPipe<TOut> operator |( Pipe<TOut> pipe, DoIf doIf ) => new DoIfPipe<TOut>(pipe);
    }

    public class DoIfPipe<T> : Command<T> {
        public DoIfPipe( Pipe<T> pipe ) : base(pipe) { }
        
        [NotNull]
        public static DoIfPipeWithPredicate<T> operator |( [CanBeNull] DoIfPipe<T> doIf, [NotNull] Func<T, bool> predicate )
            => new DoIfPipeWithPredicate<T>(doIf, predicate);

        [NotNull]
        public static DoIfPipeWithPredicate<T> operator |( [CanBeNull] DoIfPipe<T> doIf, [NotNull] Func<bool> predicate )
            => new DoIfPipeWithPredicate<T>(doIf, predicate);
    }
    
    public partial class DoIfPipeWithPredicate<T> : DoIfEnum<T> {
        internal readonly Func<T, bool> Predicate;

        internal DoIfPipeWithPredicate( DoIfPipe<T> doIf, Func<T, bool> predicate ) : base(doIf) => Predicate = predicate;
        internal DoIfPipeWithPredicate( DoIfPipe<T> doIf, Func<bool> predicate )    : base(doIf) => Predicate = _ => predicate();
        internal DoIfPipeWithPredicate( DoIfPipeWithPredicate<T> doIf )             : base(doIf) => Predicate = doIf.Predicate;
    }
    
    public partial class DoIfPipeWithPredicate<T>{
        [NotNull]
        public static DoIfPipeWithPredicateSelect<T> operator |( [CanBeNull] DoIfPipeWithPredicate<T> doIf, DoSelect doSelect )
            => new DoIfPipeWithPredicateSelect<T>(doIf);
    }
    
    
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