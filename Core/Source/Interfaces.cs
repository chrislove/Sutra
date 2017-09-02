using System;
using JetBrains.Annotations;

namespace SharpPipe
{
	public interface IPipe {
		[NotNull] SharpFunc<object> Func { get; }
	}

	public interface ISharpFunc
	{
		Func<object, object> Func { get; }
	}

	public interface IOutFunc<out TOut> : ISharpFunc {
		new Func<object, TOut> Func { get; }
	}
}