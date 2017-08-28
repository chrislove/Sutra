using System;
using JetBrains.Annotations;

namespace SharpPipe {
	public sealed class ActPipe : PipeBase
	{
		public A Act { get; }
		public void Do()
		{
			if (!IsInitialized) throw new InvalidOperationException("Uninitialized pipe.");

			Act(null);
		}

		/// <summary>
		/// Returns a new pipe
		/// </summary>
		[NotNull]
		public static ActPipe APipe(A act) => new ActPipe(act);

		private ActPipe([NotNull] A act) : base(true) {
			if (act == null) throw new ArgumentNullException(nameof(act));
			Act = act;
		}


		[NotNull]
		public static implicit operator ActPipe(A act)
		{
			return new ActPipe(act);
		}
	}
}