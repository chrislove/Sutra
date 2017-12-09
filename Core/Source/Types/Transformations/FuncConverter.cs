using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Sutra.Transformations {
    internal static class FuncConverter
    {
        public static EnmConverter<TIn, TOut> Cast<TIn, TOut>( this Func<IEnumerable<TIn>, TOut> func ) => new EnmConverter<TIn, TOut>(func);
        public static Converter<TIn, TOut> Cast<TIn, TOut>( this Func<TIn, TOut> func ) => new Converter<TIn, TOut>(func);
        
        public static Converter<TOut> Cast<TOut>( this Func<TOut> func ) => new Converter<TOut>(func);
        
        public struct EnmConverter<TIn, TOut>
        {
            private readonly Func<IEnumerable<TIn>, TOut> _func;

            public EnmConverter( [NotNull] Func<IEnumerable<TIn>, TOut> func ) => _func = func ?? throw new ArgumentNullException(nameof(func));

            /// <summary>
            /// Casts function's in parameter to a given type.
            /// </summary>
            [NotNull]
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

            /// <summary>
            /// Casts function's in parameter to a given type.
            /// </summary>
            [NotNull]
            public Func<TTo, TOut> InTo<TTo>()
                {
                    Func<TIn, TOut> thisFunc = _func;
                    return i => thisFunc(i.To<TIn>($"{thisFunc.T()} InTo<{typeof(TTo)}>"));
                }
        }
        
        public struct Converter<TOut>
        {
            private readonly Func<TOut> _func;

            public Converter( [NotNull] Func<TOut> func ) => _func = func ?? throw new ArgumentNullException(nameof(func));

            /// <summary>
            /// Adds an in parameter to out-only function.
            /// </summary>
            [NotNull]
            public Func<TTo, TOut> InTo<TTo>()
                {
                    Func<TOut> func = _func;
                    
                    return i => func();
                }
        }
    }
}