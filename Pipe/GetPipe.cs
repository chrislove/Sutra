using System;
using JetBrains.Annotations;

namespace SharpPipe
{
	public class GetPipe : PipeBase
	{
		[NotNull] internal OutFunc<object> Func { get; }

		protected GetPipe( [NotNull] ISharpFunc func ) {
			if (func == null) throw new ArgumentNullException(nameof(func));

			Func = func.ToOut<object>();
		}

		[NotNull] internal static GetPipe<T> FromObject<T>(T obj) => FromFunc(OutFunc.WithValue(obj));
		[NotNull] internal static GetPipe<T> FromFunc<T>(IOutFunc<T> func) => new GetPipe<T>(func);
	}

	/*
	public abstract class GetPipe : PipeBase {
		[NotNull] internal SharpFunc Func { get; }

		protected GetPipe( [NotNull] SharpFunc func ) : base(true) {
			Func = func ?? throw new ArgumentNullException(nameof(func));
		}


		private object Get() {
			if (!IsInitialized) throw new InvalidOperationException("Uninitialized pipe.");

			return Func.Func(null);
		}


		protected T Get<T>() {
			var pipeContent = Get();

			if (!(pipeContent is T))
				throw new InvalidOperationException($"Pipe content type is '{pipeContent.GetType()}', not '{typeof(T)}'");


			return pipeContent.To<T>();
		}
	}*/
}