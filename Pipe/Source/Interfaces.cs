using System;

namespace SharpPipe {
	/*
	public interface IGetPipe {
		OutFunc<object> Func { get; }
	}*/

	public interface ISharpFunc {
		Func<object, object> Func { get; }
	}


	public interface IInFunc<in TIn> : ISharpFunc
	{
		new Func<TIn, object> Func { get; }
	}


	public interface IOutFunc<out TOut> : ISharpFunc
	{
		new Func<object, TOut> Func { get; }
	}
}