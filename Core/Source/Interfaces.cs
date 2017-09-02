using System;

namespace SharpPipe
{
	public interface ISharpFunc
	{
		Func<object, object> Func { get; }
	}

	public interface IOutFunc<out TOut> : ISharpFunc {
		new Func<object, TOut> Func { get; }
	}
}