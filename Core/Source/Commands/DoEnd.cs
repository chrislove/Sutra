using JetBrains.Annotations;

namespace SharpPipe
{
	public static partial class Pipe {
		/// <summary>
		/// Signals a pipe to return its value.
		/// </summary>
		[NotNull] public static DoEnd OUT => new DoEnd();
	}
	
	public struct DoEnd {}

	public partial class Pipe<TOut> {
		/// <summary>
		/// Forward pipe operator.
		/// </summary>
		[NotNull]
		public static TOut operator |( Pipe<TOut> lhs, DoEnd doEnd ) {
			return lhs.Get;
		}
	}
}