using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public partial struct EnumerablePipe<T> {
        [NotNull]
        public static DoIfEnum<T> operator |( EnumerablePipe<T> pipe, DoIf doIf ) => new DoIfEnum<T>(pipe);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoIfEnum<T> : Command<T> {
        protected DoIfEnum( Command<T> doIf ) : base(doIf) {}
        protected internal DoIfEnum( EnumerablePipe<T> pipe ) : base(pipe) {}

        [NotNull]
        public static DoIfEnumWithPredicate<T> operator |( [CanBeNull] DoIfEnum<T> doIf, [NotNull] Func<IEnumerable<T>, bool> predicate )
            => new DoIfEnumWithPredicate<T>(doIf, predicate);

        [NotNull]
        public static DoIfEnumWithPredicate<T> operator |( [CanBeNull] DoIfEnum<T> doIf, [NotNull] Func<bool> predicate )
            => new DoIfEnumWithPredicate<T>(doIf, predicate);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoIfEnumWithPredicate<T> : DoIfEnum<T> {
        internal readonly Func<IEnumerable<T>, bool> Predicate;

        internal DoIfEnumWithPredicate( DoIfEnum<T> doIf, Func<IEnumerable<T>, bool> predicate ) : base(doIf) => Predicate = predicate;
        internal DoIfEnumWithPredicate( DoIfEnum<T> doIf, Func<bool> predicate )                 : base(doIf) => Predicate = _ => predicate();
        protected DoIfEnumWithPredicate( DoIfEnumWithPredicate<T> doIf )                         : base(doIf) => Predicate = doIf.Predicate;

        [NotNull]
        public static DoIfEnumWithPredicateSelect<T> operator |( [CanBeNull] DoIfEnumWithPredicate<T> doIf, DoSelect doSelect )
            => new DoIfEnumWithPredicateSelect<T>(doIf);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DoIfEnumWithPredicateSelect<T> : DoIfEnumWithPredicate<T> {
        internal DoIfEnumWithPredicateSelect( DoIfEnumWithPredicate<T> doIf ) : base(doIf) {}
        
        public static EnumerablePipe<T> operator |( [NotNull] DoIfEnumWithPredicateSelect<T> doSelectPipe, [NotNull] Func<T, T> func ) {
            if (doSelectPipe == null) throw new ArgumentNullException(nameof(doSelectPipe));
            if (func == null) throw new ArgumentNullException(nameof(func));
            
            var pipe = (EnumerablePipe<T>) doSelectPipe.Pipe;
            
            if (doSelectPipe.Predicate(pipe.Get))
                return pipe.Get.Select(func) | TO<T>.PIPE;

            return pipe;
        }
    }
}