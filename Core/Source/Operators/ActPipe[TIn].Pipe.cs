using JetBrains.Annotations;

namespace SharpPipe {
	public sealed partial class ActPipe<TIn> {
		/// <summary>
		/// Forward pipe operator
		/// </summary>
		[NotNull]
		[UsedImplicitly]
		public static ActPipe<TIn> operator |( ActPipe<TIn> lhs, PipeEnd pipeEnd ) {
			//Do nothing
			return lhs;
		}
	}
}