using System;
using JetBrains.Annotations;

namespace SharpPipe
{
    internal static class FuncTransformations
    {
        [Pure] [ContractAnnotation("func:null => null")]
        public static Func<TIn, TOut> Lower<TIn, TOut>([CanBeNull] this Func<Option<TIn>, Option<TOut>> func, [CanBeNull] TOut defaultOut )
            {
                if (func == null) return null;
                
                return i => func(i.ToOption()).ValueOr(defaultOut);
            }
		
        [Pure] [ContractAnnotation("func:null => null")] 
        public static Func<Option<TIn>, TOut> LowerOut<TIn, TOut>([CanBeNull] this Func<Option<TIn>, Option<TOut>> func, [CanBeNull] TOut defaultOut )
            {
                if (func == null) return null;

                return i => func(i).ValueOr(defaultOut);
            }
        
        [Pure] [ContractAnnotation("null => null")]
        public static Func<Option<T>, Option<U>> Map<T, U>   ( [CanBeNull] this Func<T, U> func )
            {
                if (func == null) return null;
                
                return i => i.Map(func);
            }
    }
}