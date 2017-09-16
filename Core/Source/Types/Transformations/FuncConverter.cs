using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpPipe.Transformations {
    internal static class FuncConverter
    {
        public static EnmConverter<TIn, TOut> Cast<TIn, TOut>( this Func<IEnumerable<TIn>, TOut> func ) => new EnmConverter<TIn, TOut>(func);
        public static Converter<TIn, TOut> Cast<TIn, TOut>( this Func<TIn, TOut> func ) => new Converter<TIn, TOut>(func);
        
        public struct EnmConverter<TIn, TOut>
        {
            private readonly Func<IEnumerable<TIn>, TOut> _func;

            public EnmConverter( [NotNull] Func<IEnumerable<TIn>, TOut> func ) => _func = func ?? throw new ArgumentNullException(nameof(func));

            public Func<IEnumerable<TTo>, TOut> InTo<TTo>()
                {
                    var @this = this;
                    return enm => @this._func(enm.Cast<TIn>());
                }
        }
        
        public struct Converter<TIn, TOut>
        {
            private readonly Func<TIn, TOut> _func;

            public Converter( [NotNull] Func<TIn, TOut> func ) => _func = func ?? throw new ArgumentNullException(nameof(func));

            public Func<TTo, TOut> InTo<TTo>()
                {
                    Func<TIn, TOut> thisFunc = _func;
                    return i => thisFunc(i.To<TIn>($"{thisFunc.T()} InTo<{typeof(TTo)}>"));
                }
        }
    }
}