using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed class ActPipe : PipeBase {
		internal A Act { get; }
		internal void Do()
		{
			if (!IsInitialized) throw new InvalidOperationException("Uninitialized pipe.");

			Act(null);
		}

		internal ActPipe([NotNull] A act) : base(true) {
			Act = act ?? throw new ArgumentNullException(nameof(act));
		}

		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		[UsedImplicitly]
		public static ActPipe operator |(ActPipe lhs, PipeEnd pipeEnd)
		{
			//Do nothing
			return lhs;
		}
	}
}