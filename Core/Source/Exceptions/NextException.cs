using System;
using JetBrains.Annotations;
using Sutra.Transformations;
using static Sutra.Commands;

namespace Sutra
{
    internal static class NextException
    {
        [ThreadStatic] private static Option<Func<Exception>> _factory;

        public static Option<Exception> Get()
            {
                try
                    {
                        foreach (var factory in _factory.Enm)
                            return factory().ToOption();

                        return none<Exception>();
                    } finally {
                        _factory = none<Func<Exception>>();
                    }
            }

        public static void SetFactory( [NotNull] Func<Exception> factory )
            {
                if (factory == null) throw new ArgumentNullException(nameof(factory));

                _factory = factory.ToOption();
            }

        public static void Reset() => _factory = none<Func<Exception>>();
    }
}