﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static SharpPipe.Commands;

namespace SharpPipe {
    public static partial class FuncFactory {
        public static partial class func {
            public partial class takes<TIn> {
                public static ToSeqFunc<TIn, TOut> toseq<TOut>( [NotNull] Func<TIn, IEnumerable<TOut>> func ) => new ToSeqFunc<TIn, TOut>(func.ToSeqBind());
            }
        }
    }

    /// <summary>
    /// Function transforming pipe to a sequence.
    /// </summary>
    [PublicAPI]
    public struct ToSeqFunc<TIn, TOut> {
        /// <summary>
        /// Inner function
        /// </summary>
        [NotNull] public Func<Option<TIn>, SeqOption<TOut>> Func { get; }

        /// <summary>
        /// Use this operator to invoke the function.
        /// </summary>
        public SeqOption<TOut> this[ [CanBeNull] TIn invalue ] => Func(invalue.ToOption());
        public SeqOption<TOut> this[ Option<TIn> invalue ] => Func(invalue);
        
        internal ToSeqFunc([NotNull] Func<Option<TIn>, SeqOption<TOut>> func) => Func = func ?? throw new ArgumentNullException(nameof(func));
        internal ToSeqFunc([NotNull] Func<TIn, IEnumerable<TOut>> func) => Func = option => option.Map(func).Return();

        
        /// <summary>
        /// Returns the contained function.
        /// </summary>
        [NotNull]
        public static Func<Option<TIn>, SeqOption<TOut>> operator !( ToSeqFunc<TIn, TOut> pipeFunc ) => pipeFunc.Func;

        
        /// <summary>
        /// Transforms pipe to a sequence using function on the right.
        /// </summary>
        public static Seq<TOut> operator |( Pipe<TIn> pipe, ToSeqFunc<TIn, TOut> func ) => start<TOut>.seq | func[pipe.Option];
        
        [NotNull]
        public static implicit operator Func<Option<TIn>, SeqOption<TOut>>( ToSeqFunc<TIn, TOut> pipeFunc ) => pipeFunc.Func;
        public static implicit operator ToSeqFunc<TIn, TOut>( [NotNull] Func<TIn, IEnumerable<TOut>> func ) => new ToSeqFunc<TIn, TOut>(func);
        public static implicit operator ToSeqFunc<TIn, TOut>( [NotNull] Func<Option<TIn>, SeqOption<TOut>> func ) => new ToSeqFunc<TIn, TOut>(func);
    }
}