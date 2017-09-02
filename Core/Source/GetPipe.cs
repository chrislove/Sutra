using System;
using JetBrains.Annotations;

namespace SharpPipe
{
	public abstract class GetPipe
	{
		[NotNull] internal SharpFunc<object> Func { get; }

		protected GetPipe( [NotNull] ISharpFunc func ) {
			if (func == null) throw new ArgumentNullException(nameof(func));

			Func = func.ToOut<object>();
		}

		[NotNull] internal static GetPipe<T> FromObject<T>(T obj) => FromFunc(SharpFunc.WithValue(obj));
		[NotNull] internal static GetPipe<T> FromFunc<T>(IOutFunc<T> func) => new GetPipe<T>(func);
	}
}