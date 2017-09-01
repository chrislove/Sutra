using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed partial class ActPipe<TIn> : PipeBase {
		internal Action<TIn> Action { get; }
		internal void Do()
		{
			Action( default );
		}

		internal ActPipe([NotNull] Action<TIn> act) {
			Action = act ?? throw new ArgumentNullException(nameof(act));
		}
	}
}