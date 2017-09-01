using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed class ActPipe<TIn> : PipeBase {
		internal Action<TIn> Action { get; }
		internal void Do()
		{
			Action( default );
		}

		internal ActPipe([NotNull] Action<TIn> act) {
			Action = act ?? throw new ArgumentNullException(nameof(act));
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		[UsedImplicitly]
		public static ActPipe<TIn> operator |(ActPipe<TIn> lhs, PipeEnd pipeEnd)
		{
			//Do nothing
			return lhs;
		}
	}
}