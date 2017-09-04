using System;
using JetBrains.Annotations;

namespace SharpPipe
{
	public interface ISharpFunc
	{
		[NotNull] Func<object, object> Func { get; }
	}

	public interface IOutFunc<out TOut> : ISharpFunc {
		[NotNull] new Func<object, TOut> Func { get; }
	}
}