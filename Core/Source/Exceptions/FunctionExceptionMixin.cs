using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using SharpPipe.Transformations;

namespace SharpPipe {
    internal static class FunctionExceptionMixin
    {
        [NotNull]
        private static readonly ConditionalWeakTable<object, Func<Exception>> _weakTable = new ConditionalWeakTable<object, Func<Exception>>();

        public static void AttachException( [NotNull] this object obj, [NotNull] Func<Exception> excFactory )
            {
                if (obj == null) throw new ArgumentNullException(nameof(obj));
                if (excFactory == null) throw new ArgumentNullException(nameof(excFactory));

                _weakTable.GetValue(obj, i => excFactory);
            }

        public static Option<Exception> TryGetException( [NotNull] this object obj )
            {
                return obj.TryGetExceptionFactory().Map( i => i() );
            }
        
        [CanBeNull]
        public static Option<Func<Exception>> TryGetExceptionFactory( [NotNull] this object obj )
            {
                if (obj == null) throw new ArgumentNullException(nameof(obj));
                
                _weakTable.TryGetValue(obj, out Func<Exception> factory);

                return factory.ToOption();
            }
        
        [NotNull]
        public static T CopyExceptionFrom<T>( [NotNull] this T obj, [NotNull] object copyFrom )
            {
                if (obj == null) throw new ArgumentNullException(nameof(obj));
                if (copyFrom == null) throw new ArgumentNullException(nameof(copyFrom));

                Option<Func<Exception>> srcFactory = copyFrom.TryGetExceptionFactory();
                
                foreach (Func<Exception> func in srcFactory.Enm)
                    copyFrom.AttachException( func );

                return obj;
            }
    }
}