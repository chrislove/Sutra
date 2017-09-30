using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SharpPipe.Transformations;

namespace SharpPipe
{
    public static partial class Commands
    {
        public static PipeFunc<TIn, TOut>     fun<TIn, TOut>(Func<TIn, TOut> func)  => new PipeFunc<TIn, TOut>(func);
        public static PipeFunc<string, TOut>  fun<TOut>( Func<string, TOut> func )  => fun<string, TOut>(func);
        public static PipeFunc<int, TOut>     fun<TOut>( Func<int, TOut> func )     => fun<int, TOut>(func);
        public static PipeFunc<bool, TOut>    fun<TOut>( Func<bool, TOut> func )    => fun<bool, TOut>(func);
        public static PipeFunc<float, TOut>   fun<TOut>( Func<float, TOut> func )   => fun<float, TOut>(func);
        public static PipeFunc<double, TOut>  fun<TOut>( Func<double, TOut> func )  => fun<double, TOut>(func);
        
        public static PipeFunc<TIn, TOut>     fun<TIn, TOut>(Func<Option<TIn>, Option<TOut>> func)  => new PipeFunc<TIn, TOut>(func);
        public static PipeFunc<string, TOut>  fun<TOut>( Func<Option<string>, Option<TOut>> func )  => fun<string, TOut>(func);
        public static PipeFunc<int, TOut>     fun<TOut>( Func<Option<int>, Option<TOut>> func )     => fun<int, TOut>(func);
        public static PipeFunc<bool, TOut>    fun<TOut>( Func<Option<bool>, Option<TOut>> func )    => fun<bool, TOut>(func);
        public static PipeFunc<float, TOut>   fun<TOut>( Func<Option<float>, Option<TOut>> func )   => fun<float, TOut>(func);
        public static PipeFunc<double, TOut>  fun<TOut>( Func<Option<double>, Option<TOut>> func )  => fun<double, TOut>(func);
        
        
        
        public static ToSeqFunc<TIn, TOut>    toseq<TIn, TOut>(Func<TIn, IEnumerable<TOut>> func)  => new ToSeqFunc<TIn, TOut>(func);
        public static ToSeqFunc<string, TOut> toseq<TOut>( Func<string, IEnumerable<TOut>> func )  => toseq<string, TOut>(func);
        public static ToSeqFunc<int, TOut>    toseq<TOut>( Func<int, IEnumerable<TOut>> func )     => toseq<int, TOut>(func);
        public static ToSeqFunc<bool, TOut>   toseq<TOut>( Func<bool, IEnumerable<TOut>> func )    => toseq<bool, TOut>(func);
        public static ToSeqFunc<float, TOut>  toseq<TOut>( Func<float, IEnumerable<TOut>> func )   => toseq<float, TOut>(func);
        public static ToSeqFunc<double, TOut> toseq<TOut>( Func<double, IEnumerable<TOut>> func )  => toseq<double, TOut>(func);
        
        public static ToSeqFunc<TIn, TOut>    toseq<TIn, TOut>(Func<Option<TIn>, SeqOption<TOut>> func)  => new ToSeqFunc<TIn, TOut>(func);
        public static ToSeqFunc<string, TOut> toseq<TOut>( Func<Option<string>, SeqOption<TOut>> func )  => toseq<string, TOut>(func);
        public static ToSeqFunc<int, TOut>    toseq<TOut>( Func<Option<int>, SeqOption<TOut>> func )     => toseq<int, TOut>(func);
        public static ToSeqFunc<bool, TOut>   toseq<TOut>( Func<Option<bool>, SeqOption<TOut>> func )    => toseq<bool, TOut>(func);
        public static ToSeqFunc<float, TOut>  toseq<TOut>( Func<Option<float>, SeqOption<TOut>> func )   => toseq<float, TOut>(func);
        public static ToSeqFunc<double, TOut> toseq<TOut>( Func<Option<double>, SeqOption<TOut>> func )  => toseq<double, TOut>(func);

        [PublicAPI]
        public static class fromseq<TIn>
        {
            public static FromSeqFunc<TIn, TOut> fun<TOut>( Func<IEnumerable<TIn>, TOut> func )       => fun(func.Map());
            public static FromSeqFunc<TIn, TOut> fun<TOut>( Func<SeqOption<TIn>, Option<TOut>> func ) => new FromSeqFunc<TIn, TOut>(func);
        }
    }
}