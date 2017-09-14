using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace SharpPipe {
    internal static class FunctionExceptionMixin
    {
        [NotNull]
        private static readonly ConditionalWeakTable<object, Func<Exception>> _weakTable = new ConditionalWeakTable<object, Func<Exception>>();

        [NotNull]
        public static void AttachException( [NotNull] this object obj, [NotNull] Func<Exception> excFactory )
            {
                if (obj == null) throw new ArgumentNullException(nameof(obj));
                if (excFactory == null) throw new ArgumentNullException(nameof(excFactory));

                _weakTable.GetValue(obj, i => excFactory);
            }

        [CanBeNull]
        public static Exception TryGetException( [NotNull] this object obj )
            {
                if (obj == null) throw new ArgumentNullException(nameof(obj));
                
                _weakTable.TryGetValue(obj, out Func<Exception> exc);

                return exc?.Invoke();
            }
    }
}