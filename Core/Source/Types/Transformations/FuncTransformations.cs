using System;
using JetBrains.Annotations;

namespace SharpPipe
{
    internal static class FuncTransformations
    {
        /// <summary>
        /// Lowers function to accept and return non-option values.
        /// </summary>
        [Pure] [ContractAnnotation("func:null => null")]
        public static Func<TIn, TOut> Fold<TIn, TOut>([CanBeNull] this Func<Option<TIn>, Option<TOut>> func, [CanBeNull] TOut defaultOut )
            {
                if (func == null) return null;
                
                return i => func(i.ToOption()).ValueOr(defaultOut);
            }
		
        /// <summary>
        /// Lowers the output of a function to return non-option values.
        /// </summary>
        [Pure] [ContractAnnotation("func:null => null")] 
        public static Func<Option<TIn>, TOut> LowerOut<TIn, TOut>([CanBeNull] this Func<Option<TIn>, Option<TOut>> func, [CanBeNull] TOut defaultOut )
            {
                if (func == null) return null;

                return i => func(i).ValueOr(defaultOut);
            }
        
        /// <summary>
        /// Lifts function to accept and return Option.
        /// </summary>
        [Pure] [ContractAnnotation("null => null")]
        public static Func<Option<T>, Option<U>> Map<T, U>   ( [CanBeNull] this Func<T, U> func )
            {
                if (func == null) return null;
                
                return i => i.Map(func);
            }
        
        /*
        /// <summary>
        /// Lifts function to accept and return Option.
        /// </summary>
        [Pure] [ContractAnnotation("null => null")]
        public static Func<Option<T>, Option<U>> Map<T, U>   ( [CanBeNull] this Func<IEnumerable<T>, U> func )
            {
                if (func == null) return null;
                
                return i => i.Map(func);
            }*/

        /// <summary>
        /// Converts action into function returning Unit.
        /// </summary>
        [Pure]
        [ContractAnnotation("null => null")]
        public static Func<T, Unit> ReturnUnit<T>( [CanBeNull] this Action<T> act ) => i =>
                                                                                        {
                                                                                            act(i);
                                                                                            return Commands.unit;
                                                                                        };

        /// <summary>
        /// Lifts action to accept Option{T} parameter.
        /// </summary>
        [Pure] [ContractAnnotation("null => null")]
        public static Action<Option<T>> Map<T>( [CanBeNull] this Action<T> act )
            {
                if (act == null) return null;
                
                return i => i.Map(act.ReturnUnit());
            }
    }
}