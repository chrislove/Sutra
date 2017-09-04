using JetBrains.Annotations;

namespace SharpPipe
{
	public static partial class Commands {
		/// <summary>
		/// Signals a pipe to return its value.
		/// </summary>
		public static DoEnd OUT => new DoEnd();
	}
	
	public struct DoEnd {}

	public partial struct Pipe<TOut> {
		/// <summary>
		/// Forward pipe operator.
		/// </summary>
		[NotNull]
		public static TOut operator |( Pipe<TOut> lhs, DoEnd doEnd ) => lhs.Get;
	}
}