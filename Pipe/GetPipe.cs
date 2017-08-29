using System;

namespace SharpPipe {
	public class GetPipe : PipeBase {
		protected GetPipe( bool isInitialized ) : base(isInitialized) {}

		internal F Func { get; set; }

		private object Get() {
			if (!IsInitialized) throw new InvalidOperationException("Uninitialized pipe.");

			return Func(null);
		}


		protected T Get<T>() {
			var pipeContent = Get();

			if (!(pipeContent is T))
				throw new InvalidOperationException($"Pipe content type is '{pipeContent.GetType()}', not '{typeof(T)}'");


			return pipeContent.To<T>();
		}
	}
}