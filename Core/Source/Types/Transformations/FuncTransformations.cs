﻿using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe.Transformations
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
        
        /// <summary>
        /// Lifts action to accept Option{T} parameter and return Unit.
        /// </summary>
        [Pure] [ContractAnnotation("null => null")]
        public static Func<Option<T>, Unit> Map<T>( [CanBeNull] this Action<T> act )
            {
                return i => i.Match(act.ReturnsUnit(), unit);
            }
        
        
        /// <summary>
        /// Lifts unit function to accept SeqOption{T}.
        /// </summary>
        [Pure] [ContractAnnotation("null => null")]
        public static Func<SeqOption<T>, Unit> Map<T>( [CanBeNull] this Func<Option<T>, Unit> func )
            {
                Func<IEnumerable<Option<T>>, Unit> enmFunc = enm => enm.Select(func).Aggregate(( a, b ) => unit);
                
                return seq => enmFunc.Map()(seq).ValueOr(unit);
            }
        
        public static Func<T, Unit> ReturnsUnit<T>([NotNull] this Action<T> act) => i =>
                                                                                          {
                                                                                              act(i);
                                                                                              return unit;
                                                                                          };
    }
}